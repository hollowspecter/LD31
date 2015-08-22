﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behaviour : ParentNode  
{
	BehaviourTree tree;

	GUIBehaviour view;

	bool isRunning = false;
	ChildNode behaviourRoot;
	List<TaskNode> activeTasks;
	
	float timer = 0.0f;
	
	public Behaviour(GUIBehaviour view, BehaviourTree tree)
	{
		activeTasks = new List<TaskNode>();
		Debug.Log ("Behaviour constructed");

		this.view = view;
		this.view.SetModel(this);

		this.tree = tree;
	}

	// Use this for initialization
	public void StartBehaviour() 
	{
		Debug.Log("go");
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
		if((!isRunning ||activeTasks.Count < 1) && timer > 1.0f)
		{
			
			Debug.Log("Start BehaviourTree");
			StartBehaviour();
			timer = 0.0f;
		}
		timer += Time.deltaTime;
		for(int i = 0; i< activeTasks.Count; ++i)
		{
			activeTasks[i].PerformTask();
		}

	}

	public void AddChild(ChildNode child)
	{
		behaviourRoot = child;
	}

	public void RemoveChild(ChildNode child)
	{
		behaviourRoot = null;
	}

	public void ChildDone(ChildNode child, bool childResult)
	{
		Debug.Log("Behaviour terminated with Result: " + childResult); 
		isRunning = false;
		Debug.Log ("isrunning:" + isRunning);
	}

	public void activateTask(TaskNode t)
	{
		activeTasks.Add(t);
	}

	public void deactivateTask(TaskNode t)
	{
		activeTasks.Remove(t);
	}

	public void Delete()
	{
		if(behaviourRoot != null)
		{
			behaviourRoot.Delete();
			behaviourRoot = null;
		}
		activeTasks.Clear();

		tree.SetBehaviour(null);
	}

	public void ChildEvent(ChildNode child)
	{
		
	}

	/*******GETTER & SETTER****************/

	public List<TaskNode> GetActiveTasks()
	{
			return activeTasks;
	}

	public bool hasRoot()
	{
		return (behaviourRoot != null); 
	}

	public ChildNode GetRoot()
	{
		return behaviourRoot;
	}

	public void SetIsRunning(bool b)
	{
		isRunning = b;
	}

	public bool GetIsRunning()
	{
		return isRunning;
	}

	public GUINode GetView()
	{
		return view;
	}
}
