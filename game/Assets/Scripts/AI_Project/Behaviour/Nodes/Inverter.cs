using UnityEngine;
using System.Collections;

public class Inverter : ChildNode, ParentNode {

	ParentNode parent;
	
	ChildNode child;
	
	public Inverter(ParentNode parent)
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
		parent.ChildDone(this, !childResult);		
	}
	
	public void Activate()
	{
		if(child != null)
		{
			child.Activate();
		}
	}
}
