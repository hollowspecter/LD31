using UnityEngine;
using System.Collections;

public class SeekOpponentTask : TaskNode 
{
	Behaviour rootBehaviour;

	ParentNode parent;

	AI_Movement moveComponent;
	GameObject opponent;

	public SeekOpponentTask(ParentNode parent, Behaviour rootBehaviour)
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
		Debug.Log ("seektask activate");


		rootBehaviour.activateTask(this);
		moveComponent.SetTarget(opponent.transform);
		moveComponent.setSeeking(true);
	}

	public void Deactivate()
	{
		moveComponent.SetTarget(null);
		moveComponent.setSeeking(false);
		moveComponent.stop();
		rootBehaviour.deactivateTask(this);

	}
	
	public void PerformTask()
	{
		//has the AI arrived at its target?
		if(moveComponent.getHasArrived())
		{
			Debug.Log ("seektask suceed: arrived");
			Deactivate();
			parent.ChildDone(this, true);
		}
		//is the AI or the player falling off the board?
		else if(!moveComponent.getOnFloor() )
		{
			Debug.Log ("seektask failp: AI falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		else if(!opponent.GetComponent<PlayerMovement>().getOnFloor())
		{
			Debug.Log ("seektask fail: Player falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//if neither: seek the target through subtargets
		else
		{
			moveComponent.attr_SeekSubtarget();
		}
	}

}
