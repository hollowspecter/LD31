using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UntilFail: ChildNode, ParentNode 
{
	ParentNode parent;

	GUIUntilFail view;
	
	ChildNode child;

	public UntilFail(ParentNode parent, GUIUntilFail view)
	{
		Debug.Log ("Untilfail constructed");
		this.parent = parent;
		this.parent.AddChild(this);

		this.view = view;
		this.view.SetModel(this);
	}

	public void AddChild(ChildNode child)
	{
		this.child = child;
	}

	public void RemoveChild(ChildNode child)
	{
		this.child = null;
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
		if(child != null)
			child.Deactivate();
	}

	public void ChildEvent(ChildNode child)
	{
		
	}

	public GUINode GetView()
	{
		return view;
	}

	public void Delete()
	{
		if(child != null)
			child.Delete();
		parent.RemoveChild(this);
	}

	public ParentNode GetParent()
	{
		return parent;
	}

	public ChildNode GetChild()
	{
		return child;
	}

}
