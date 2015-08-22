using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : ChildNode, ParentNode 
{
	GUISelector view;

	ParentNode parent;

	List<ChildNode> children;
	int currentChildIndex;

	// Use this for initialization
	public Selector(ParentNode parent, GUISelector view)
	{
		this.parent = parent;
		this.parent.AddChild(this);

		children = new List<ChildNode>();

		this.view = view;
		this.view.SetModel(this);
	}

	public void AddChild(ChildNode child)
	{
		children.Add(child);
	}

	public void RemoveChild(ChildNode child)
	{
		children.Remove(child);
	}

	public void ChildDone(ChildNode child, bool childResult)
	{
		
		//if child returns true, return true yourself
		if(childResult)
		{
			parent.ChildDone(this, true);
		}
			
		//else try the next child if there is no next child return false
		else
		{
			currentChildIndex++;
			if(currentChildIndex < children.Count)
			{
				Debug.Log("Selector: next");
				children[currentChildIndex].Activate();
			}
			else
			{
				Debug.Log("Selector: stop");
				parent.ChildDone(this, false);
			}
		}

	}

	public void Activate()
	{
		if(children.Count > 0)
		{
			currentChildIndex = 0;
			children[currentChildIndex].Activate();
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


	/***********GETTER & SETTER********************/
	public ParentNode GetParent()
	{
		return parent;
	}

	public List<ChildNode> GetChildren()
	{
		return children;
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
		
		parent.RemoveChild(this);
	}


}
