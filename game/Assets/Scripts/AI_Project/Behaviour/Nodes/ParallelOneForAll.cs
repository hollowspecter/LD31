using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is a special Parallelclass that stops all its childrenTasks, no matter what the result is
public class ParallelOneForAll : ChildNode, ParentNode 
{
	ParentNode parent;
	
	List<ChildNode> children;
	List<bool> returns;
	
	// Use this for initialization
	public ParallelOneForAll(ParentNode parent)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		Debug.Log("parallel constructed");
		children = new List<ChildNode>();
		returns = new List<bool>();
	}
	
	public void AddChild(ChildNode child)
	{
		children.Add(child);
		returns.Add(new bool());
	}

	public void RemoveChild(ChildNode child)
	{
		int i = children.IndexOf(child);
		returns.RemoveAt(i);
		children.Remove(child);
	}
	
	public void ChildDone(ChildNode child, bool childResult)
	{
			for(int i = 0; i < children.Count; i++)
			{
				children[i].Deactivate();
			}
			parent.ChildDone(this, childResult);
		
	}
	
	public void Activate()
	{
		if(children.Count > 0)
		{
			for(int i = 0; i < children.Count; ++i)
			{
				returns[i] = false;
				children[i].Activate();
			}
		}
		else
		{
			Debug.Log("Parallel: no children");
		}
	}
	
	public void Deactivate()
	{
		for(int i = 0; i < children.Count; ++i)
		{
			children[i].Deactivate();
		}
	}

	public GUINode GetView()
	{
		return null;
	}

	public void Delete()
	{
		for(int i = 0; i < children.Count; ++i)
		{
			children[i].Delete();
		}
		children.Clear();
		returns.Clear();
		
		parent.RemoveChild(this);
	}

	public ParentNode GetParent()
	{
		return parent;
	}
}
