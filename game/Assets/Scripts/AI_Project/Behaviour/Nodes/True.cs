using UnityEngine;
using System.Collections;

public class True : ChildNode, ParentNode 
{
	
	ParentNode parent;

	GUITrue view;
	
	ChildNode child;
	
	public True(ParentNode parent, GUITrue view)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		this.view = view;
		view.SetModel(this);
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

