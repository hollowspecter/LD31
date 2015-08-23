using UnityEngine;
using System.Collections;

//Flanking Task
//This Task makes the AI flank around the Opponent left or right Randomly
//for a specified time, until the Opponent leaves the flanking range or until the usual Criteria
//like falling apply

public class FlankOpponentTask : TaskNode 
{
	public static int TypeID = 12;

	Behaviour rootBehaviour;

	GUIFlankOpponentTask view;
	
	ParentNode parent;
	
	AI_Movement moveComponent;
	GameObject opponent;

	float timer = 0.0f; //How long are you flanking
	float maxTimer = 1.0f; //How long are you allowed to flank
	bool flankDirection; //are you going right?
	float maxFlankDistance  = 9.0f; //How far away can you be to continue flanking

	public FlankOpponentTask(ParentNode parent, Behaviour rootBehaviour, float flankDistance, GUIFlankOpponentTask view)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		this.rootBehaviour = rootBehaviour;

		maxFlankDistance = flankDistance;

		this.view = view;
		this.view.SetModel(this);
		
		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}		
		moveComponent = self.GetComponent<AI_Movement>();
		
		opponent = GameObject.FindGameObjectWithTag("Player0");
		if(opponent == null)
		{
			opponent = GameObject.Find("boar");
		}
	}

	public FlankOpponentTask()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create FlankOpponentTask");
		GUIFlankOpponentTask gui = new GUIFlankOpponentTask(nodeID, guiPosition);
		tree.AddGUINode(gui);
		
		this.parent = parent;
		this.parent.AddChild(this);
		
		this.view = gui;
		this.view.SetModel(this);

		this.rootBehaviour = rootBehaviour;

		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}		
		moveComponent = self.GetComponent<AI_Movement>();
		
		opponent = GameObject.FindGameObjectWithTag("Player0");
		if(opponent == null)
		{
			opponent = GameObject.Find("boar");
		}
		
		tree.AddNode(this);
	}
	
	public void Activate()
	{
		timer = 0.0f;
		Debug.Log ("flanktask activate");

		flankDirection = Random.value > 0.5?true:false;
		moveComponent.SetTarget(opponent.transform);
		moveComponent.SetTarget(null);
		rootBehaviour.activateTask(this);
		moveComponent.setFlanking(true);
	}
	
	public void Deactivate()
	{
		Debug.Log ("flanktask deactivate");
		moveComponent.stop();
		rootBehaviour.deactivateTask(this);
		moveComponent.setFlanking(false);
		
	}
	
	public void PerformTask()
	{
		//has the AI arrived at its target?
		if(timer >= maxTimer)
		{
			moveComponent.turnToTarget();
			Debug.Log ("flanktask suceed: flanktimer achieved");
			Deactivate();
			parent.ChildDone(this, true);
		}
		//is the AI or the player falling off the board?
		else if(!moveComponent.getOnFloor())
		{
			Debug.Log ("flanktask fail: AI falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		else if(!opponent.GetComponent<PlayerMovement>().getOnFloor())
		{
			Debug.Log ("flanktask fail: Player falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		else if((opponent.transform.position - moveComponent.transform.position).sqrMagnitude > maxFlankDistance)
		{
			Debug.Log ("flanktask fail: Target too far away");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//if neither: seek the target through subtargets
		else if(timer < maxTimer)
		{
			moveComponent.SetTarget(opponent.transform);
			moveComponent.attr_flank(flankDirection);
			timer += Time.deltaTime;
		}
	}

	public GUINode GetView()
	{
		return view;
	}

	public void Delete()
	{
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
