using UnityEngine;
using System.Collections;

public class FlankOpponentTask : TaskNode 
{
	Behaviour rootBehaviour;
	
	ParentNode parent;
	
	AI_Movement moveComponent;
	GameObject opponent;

	float timer = 0.0f;
	float maxTimer = 1.0f;
	bool flankDirection;
	float maxFlankDistance  = 9.0f;

	public FlankOpponentTask(ParentNode parent, Behaviour rootBehaviour, float flankDistance)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		this.rootBehaviour = rootBehaviour;
		maxFlankDistance = flankDistance;
		
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
	
}
