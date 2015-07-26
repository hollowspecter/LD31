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
		moveComponent.stop();
		rootBehaviour.deactivateTask(this);
		
	}
	
	public void PerformTask()
	{
		float sqrDistToOpponent = (opponent.transform.position - moveComponent.transform.position).sqrMagnitude;
		//are you a safe distance away from the opponent?
		if(sqrDistToOpponent > safeSqrDist)
		{
			//do nothing
			Debug.Log ("evadetask succeed: AI safe");
			//Deactivate();
			//parent.ChildDone(this, true);
		}
		//are you a decent distance away?
		else if(sqrDistToOpponent > dangerSqrDist)
		{
			//move away until you are safe
			moveComponent.rep_evadePosition(opponent.transform, safeSqrDist);
		}
		//are you a dangerous distance away?
		else
		{
			Debug.Log ("evadetask fail: Player to close");
			moveComponent.rep_evadePosition(opponent.transform, safeSqrDist);
			//Deactivate();
			//parent.ChildDone(this, false);
		}
	}
	
}
