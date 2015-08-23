using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UntilFail: DecoratorNode
{
	public static int TypeID = 8;

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

	public UntilFail()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create UntilFail");
		GUIUntilFail gui = new GUIUntilFail(nodeID, guiPosition);
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

	public int GetTypeID()
	{
		return TypeID;
	}

}
