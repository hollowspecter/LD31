using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behaviour : ParentNode 
{
	bool isRunning = false;
	ChildNode behaviourRoot;
	List<TaskNode> activeTasks;
	
	public Behaviour()
	{
		activeTasks = new List<TaskNode>();
		Debug.Log ("Behaviour constructed");
	}

	// Use this for initialization
	public void StartBehaviour() 
	{
		if(behaviourRoot != null)
		{
			Debug.Log ("Behaviour started");
			behaviourRoot.Activate();
			isRunning = true;
		}
		else
		{
			Debug.Log ("Behaviour has no root");
		}
			
	}
	
	// Update is called once per frame
	public void Update() 
	{
		if(!isRunning)
		{
			StartBehaviour();
			Debug.Log("Start BehaviourTree");
		}


		for(int i = 0; i< activeTasks.Count; ++i)
		{
			activeTasks[i].PerformTask();
		}

	}

	public void AddChild(ChildNode child)
	{
		behaviourRoot = child;
	}

	public void ChildDone(ChildNode child, bool childResult)
	{
		Debug.Log("Behaviour terminated with Result: " + childResult); 
		isRunning = false;
	}

	public void activateTask(TaskNode t)
	{
		activeTasks.Add(t);
	}

	public void deactivateTask(TaskNode t)
	{
		activeTasks.Remove(t);
	}


	public List<TaskNode> GetActiveTasks()
	{
			return activeTasks;
	}

	public void SetIsRunning(bool b)
	{
		isRunning = b;
	}

	public bool GetIsRunning()
	{
		return isRunning;
	}
}
