using UnityEngine;
using System.Collections;

public class Inverter : DecoratorNode
{
	public static int TypeID = 5;
	ParentNode parent;

	GUIInverter view;
	
	ChildNode child;
	
	public Inverter(ParentNode parent, GUIInverter view)
	{
		this.parent = parent;
		this.parent.AddChild(this);

		this.view = view;
		this.view.SetModel(this);
	}

	public Inverter()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create Inverter");
		GUIInverter gui = new GUIInverter(nodeID, guiPosition);
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

	public ChildNode GetChild()
	{
		return child;
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
	public int GetTypeID()
	{
		return TypeID;
	}

}
