using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UntilFail: ChildNode, ParentNode 
{
	ParentNode parent;
	
	ChildNode child;
	
	public void AddChild(ChildNode child)
	{
		this.child = child;
	}
	
	public void ChildDone(ChildNode child, bool childResult)
	{
		
		//if child returns true, return true yourself
		if(childResult)
		{
			child.Activate();
		}
		
		//else try the next child if there is no next child return false
		else
		{
			parent.ChildDone(this, true);
		}
		
	}
	
	public void Activate()
	{
		if(child != null)
		{
			child.Activate();
		}
	}
}
