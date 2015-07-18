using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behaviour : ParentNode 
{
	bool isRunning = false;
	ChildNode behaviourRoot;
	List<TaskNode> activeTasks;
	
	public Behaviour ()
	{
		activeTasks = new List<TaskNode>();
	}

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
			foreach(TaskNode t in activeTasks)
			{
				t.PerformTask();
			}
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

	public void activateTask(TaskNode t)
	{
		activeTasks.Add(t);
	}

	public void deactivateTast(TaskNode t)
	{
		activeTasks.Remove(t);
	}


}
