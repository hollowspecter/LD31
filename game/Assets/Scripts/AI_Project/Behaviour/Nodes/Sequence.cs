using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequence : ChildNode, ParentNode 
{
	GUISequence view;

	ParentNode parent;
	
	List<ChildNode> children;
	int currentChildIndex;
	
	// Use this for initialization
	public Sequence(ParentNode parent, GUISequence view)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		//Debug.Log("sequence constructed");
		children = new List<ChildNode>();

		this.view = view;
		this.view.SetModel(this);
	}
	
	public void AddChild(ChildNode child)
	{
		children.Add(child);
		view.SetValue(children.Count);
	}

	public void RemoveChild(ChildNode child)
	{
		children.Remove(child);
	}
	
	public void ChildDone(ChildNode child, bool childResult)
	{
		
		//if child returns true, try the next child
		//if there is no next child return true to parent
		if(childResult)
		{
			currentChildIndex++;
			if(currentChildIndex < children.Count)
			{
				Debug.Log("Sequence: next");
				children[currentChildIndex].Activate();
			}
			else
			{
				Debug.Log("Sequence: sucess");
				parent.ChildDone(this, true);
			}

		}
		
		//if child returns false return false yourself
		else
		{
			Debug.Log("Sequence: fail");
			parent.ChildDone(this, false);
		}
		
	}
	
	public void Activate()
	{
		if(children.Count > 0)
		{
			currentChildIndex = 0;
			children[currentChildIndex].Activate();
		}
	}

	public void Deactivate()
	{
		for(int i = 0; i < children.Count; ++i)
		{
			children[i].Deactivate();
		}
	}

	public ParentNode GetParent()
	{
		return parent;
	}
	
	public List<ChildNode> GetChildren()
	{
		return children;
	}
	
	public GUINode GetView()
	{
		return view;
	}

	public void Delete()
	{
		for(int i = 0; i < children.Count; ++i)
		{
			children[i].Delete();
		}
		children.Clear();
		
		parent.RemoveChild(this);
	}

}
