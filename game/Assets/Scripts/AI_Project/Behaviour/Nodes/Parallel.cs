using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallel : ChildNode, ParentNode 
{
	ParentNode parent;
	
	List<ChildNode> children;
	List<bool> returns;
	
	// Use this for initialization
	public Parallel(ParentNode parent)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		Debug.Log("parallel constructed");
		children = new List<ChildNode>();
		returns = new List<bool>();
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

	public GUINode GetView()
	{
		return null;
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
}
