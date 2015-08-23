using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallel : DecoratorNode
{
	public static int TypeID = 6;

	ParentNode parent;

	GUIParallel view;
	
	List<ChildNode> children;
	List<bool> returns;
	
	// Use this for initialization
	public Parallel(ParentNode parent, GUIParallel view)
	{
		this.parent = parent;
		this.parent.AddChild(this);

		this.view = view;
		this.view.SetModel(this);

		Debug.Log("parallel constructed");
		children = new List<ChildNode>();
		returns = new List<bool>();
	}

	public Parallel()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create Parallel");
		GUIParallel gui = new GUIParallel(nodeID, guiPosition);
		tree.AddGUINode(gui);
		
		this.parent = parent;
		this.parent.AddChild(this);
		
		children = new List<ChildNode>();
		returns = new List<bool>();
		
		this.view = gui;
		this.view.SetModel(this);
		
		tree.AddNode(this);
	}
	
	public void AddChild(ChildNode child)
	{
		children.Add(child);
		returns.Add(new bool());
	}

	public void RemoveChild(ChildNode child)
	{
		int i = children.IndexOf(child);
		returns.RemoveAt(i);
		children.Remove(child);
	}

	public void ChildDone(ChildNode child, bool childResult)
	{
		
		//if child returns true check if all children have now returned true
		//if so, return true
		if(childResult)
		{
			int index = children.IndexOf(child);
			returns[index] = true;
			
			//check if all children are true
			bool allTrue = true;
			for(int i = 0; i < returns.Count; ++i)
			{
				if(returns[i] == false)
					allTrue = false;
			}
			if(allTrue)
			{
				for(int i = 0; i < children.Count; ++i)
				{
					children[i].Deactivate();
				}
				parent.ChildDone(this, true);
			}

		}
		//if child returns false return false yourself
		else
		{
			for(int i = 0; i < children.Count; ++i)
			{
				children[i].Deactivate();
			}
			parent.ChildDone(this, false);
		}
		
	}
	
	public void Activate()
	{
		if(children.Count > 0)
		{
			for(int i = 0; i < children.Count; ++i)
			{
				returns[i] = false;
				children[i].Activate();
			}
		}
		else
		{
			Debug.Log("Parallel: no children");
		}
	}

	public void Deactivate()
	{
		for(int i = 0; i < children.Count; ++i)
		{
			children[i].Deactivate();
		}
	}

	public void ChildEvent(ChildNode child)
	{
		bool sorted = false;
		foreach(ChildNode node in children)
		{
			if(node != child)
			{
				if(child.GetView().Position.x < node.GetView().Position.x)
				{
					sorted = true;
					children.Remove(child);
					int i = children.IndexOf(node);
					children.Insert(i, child);
					break;
				}
			}
		}
		if(!sorted)
		{
			children.Remove(child);
			children.Add (child);
		}
		foreach(ChildNode node in children)
		{
			node.GetView().setValue(children.IndexOf(node));
		}
	}

	public GUINode GetView()
	{
		return view;
	}

	public void Delete()
	{
		for(int i = 0; i < children.Count; ++i)
		{
			children[i].Delete();
		}
		children.Clear();
		returns.Clear();
		
		parent.RemoveChild(this);
	}

	public ParentNode GetParent()
	{
		return parent;
	}

	public List<ChildNode> GetChildren()
	{
		return children;
	}

	public int GetTypeID()
	{
		return TypeID;
	}
}
