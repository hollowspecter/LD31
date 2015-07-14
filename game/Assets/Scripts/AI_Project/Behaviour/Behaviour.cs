using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behaviour : ParentNode 
{
	bool isRunning;
	ChildNode behaviourRoot;
	//TODO: List<Task> activeTasks;


	// Use this for initialization
	void StartBehaviour() 
	{
		if(behaviourRoot != null)
		{
			behaviourRoot.Activate();
		}
			
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(isRunning)
		{

		}

	}

	public void AddChild(ChildNode child)
	{
		behaviourRoot = child;
	}

	public void ChildDone(ChildNode child, bool childResult)
	{
		Debug.Log("Behaviour terminated with Result: " + childResult); 
	}


}
