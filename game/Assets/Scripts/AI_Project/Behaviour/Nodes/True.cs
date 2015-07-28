using UnityEngine;
using System.Collections;

public class True : ChildNode, ParentNode 
{
	
	ParentNode parent;
	
	ChildNode child;
	
	public True(ParentNode parent)
	{
		this.parent = parent;
		this.parent.AddChild(this);
	}
	
	public void AddChild(ChildNode child)
	{
		this.child = child;
	}
	
	public void ChildDone(ChildNode child, bool childResult)
	{		
		parent.ChildDone(this, true);		
	}
	
	public void Activate()
	{
		if(child != null)
		{
			child.Activate();
		}
	}
	
	public void Deactivate()
	{
		child.Deactivate();
	}
}

