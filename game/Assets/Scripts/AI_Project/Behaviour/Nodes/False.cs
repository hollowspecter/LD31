using UnityEngine;
using System.Collections;

public class False : ChildNode, ParentNode 
{
	
	ParentNode parent;
	
	ChildNode child;
	
	public False(ParentNode parent)
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
		parent.ChildDone(this, false);		
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
