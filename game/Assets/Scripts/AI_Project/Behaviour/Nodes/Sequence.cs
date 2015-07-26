using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequence : ChildNode, ParentNode 
{
	ParentNode parent;
	
	List<ChildNode> children;
	int currentChildIndex;
	
	// Use this for initialization
	public Sequence(ParentNode parent)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		Debug.Log("sequence constructed");
		children = new List<ChildNode>();
	}
	
	public void AddChild(ChildNode child)
	{
		children.Add(child);
	}
	
	public void ChildDone(ChildNode child, bool childResult)
	{
		
		//if child returns true, try the next child
		//if there is no next child return true to parent
		if(childResult)
		{
			currentChildIndex++;
			if(currentChildIndex < children.Count)
			{
				children[currentChildIndex].Activate();
			}
			else
			{
				parent.ChildDone(this, true);
			}

		}
		
		//if child returns false return false yourself
		else
		{
			
			parent.ChildDone(this, true);
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
		
	}
}
