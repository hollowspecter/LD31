using UnityEngine;
using System.Collections;

//Evasion Task
//This Task makes the AI run away until it reaches a safe Distance or the usual Criteria
//like falling apply. 



public class EvadeOpponentTask: TaskNode 
{
	public static int TypeID = 11;

	Behaviour rootBehaviour;
	
	ParentNode parent;

	GUIEvadeOpponentTask view;
	
	AI_Movement moveComponent;
	
	GameObject opponent;

	float safeSqrDist = 60.0f;
	float dangerSqrDist = 9.0f;
	
	
	public EvadeOpponentTask(ParentNode parent, Behaviour rootBehaviour, GUIEvadeOpponentTask view)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		this.rootBehaviour = rootBehaviour;

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

	public EvadeOpponentTask()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create EvadeOpponentTask");
		GUIEvadeOpponentTask gui = new GUIEvadeOpponentTask(nodeID, guiPosition);
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
		Debug.Log ("evadetask activate");
		
		rootBehaviour.activateTask(this);
	}
	
	public void Deactivate()
	{
		Debug.Log("evadetask deactivate");
		//moveComponent.stop();
		rootBehaviour.deactivateTask(this);
		
	}


	public void PerformTask()
	{
		float sqrDistToOpponent = (opponent.transform.position - moveComponent.transform.position).sqrMagnitude;
		//are you a safe distance away from the opponent?
		if(sqrDistToOpponent > safeSqrDist)
		{
			//stop running away
			Debug.Log ("evadetask succeed: AI is safe");
			Deactivate();
			parent.ChildDone(this, true);
		}
		//is the AI or the player falling off the board?
		else if(!moveComponent.getOnFloor())
		{
			Debug.Log ("evadetask fail: AI falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		else if(!opponent.GetComponent<PlayerMovement>().getOnFloor())
		{
			Debug.Log ("evadetask succeed: Player falls");
			Deactivate();
			parent.ChildDone(this, true);
		}
		//in any other case: run away
		else
		{
			moveComponent.rep_evadePosition(opponent.transform, safeSqrDist);
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
