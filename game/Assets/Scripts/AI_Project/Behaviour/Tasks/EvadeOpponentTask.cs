using UnityEngine;
using System.Collections;


public class EvadeOpponentTask: TaskNode 
{
	Behaviour rootBehaviour;
	
	ParentNode parent;
	
	AI_Movement moveComponent;
	
	GameObject opponent;

	float safeSqrDist = 60.0f;
	float dangerSqrDist = 9.0f;
	
	
	public EvadeOpponentTask(ParentNode parent, Behaviour rootBehaviour)
	{
		this.parent = parent;
		this.parent.AddChild(this);
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
		return null;
	}

	public void Delete()
	{
		parent.RemoveChild(this);
	}
	
}
