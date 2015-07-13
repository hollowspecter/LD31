using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behaviour : ParentNode 
{
	bool isActive;
	List<ChildNode> children;
	//TODO: List<Task> activeTasks;


	// Use this for initialization
	void Start () 
	{
		children = new List<ChildNode>();

	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void AddChild(ChildNode c)
	{

	}

	public bool ChildDone(ChildNode c)
	{
		bool b = false;
		return b;
	}


}
