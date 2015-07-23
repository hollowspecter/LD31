using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : ChildNode, ParentNode 
{
	ParentNode parent;

	List<ChildNode> children;
	int currentChildIndex;

	// Use this for initialization
	public Selector(ParentNode parent)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		children = new List<ChildNode>();
	}

	public void AddChild(ChildNode child)
	{
		children.Add(child);
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
				children[currentChildIndex].Activate();
			}
			else
			{
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
}
