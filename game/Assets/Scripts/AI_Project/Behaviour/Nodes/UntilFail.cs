using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UntilFail: ChildNode, ParentNode 
{
	ParentNode parent;
	
	ChildNode child;

	public UntilFail(ParentNode parent)
	{
		Debug.Log ("Untilfail constructed");
		this.parent = parent;
		this.parent.AddChild(this);
	}

	public void AddChild(ChildNode child)
	{
		this.child = child;
	}
	
	public void ChildDone(ChildNode child, bool childResult)
	{

		//if child returns true activate it again
		if(childResult)
		{
			Debug.Log ("loop");
			child.Activate();
		}
		
		//else return true
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
	public void Deactivate()
	{
		child.Deactivate();
	}

}
