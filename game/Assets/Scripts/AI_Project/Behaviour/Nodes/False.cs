using UnityEngine;
using System.Collections;

public class False : DecoratorNode 
{
	public static int TypeID = 4;
	
	ParentNode parent;
	
	ChildNode child;

	GUIFalse view;

	public False(ParentNode parent, GUIFalse view)
	{
		this.parent = parent;
		this.parent.AddChild(this);

		this.view = view;
		this.view.SetModel(this);
	}

	public False()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create False");
		GUIFalse gui = new GUIFalse(nodeID, guiPosition);
		tree.AddGUINode(gui);
		
		this.parent = parent;
		this.parent.AddChild(this);
		
		this.view = gui;
		this.view.SetModel(this);
		
		tree.AddNode(this);
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

	public ChildNode GetChild()
	{
		return child;
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

	public int GetTypeID()
	{
		return TypeID;
	}
}
