using UnityEngine;
using System.Collections;

public class MaintainDistanceTask : TaskNode 
{
	Behaviour rootBehaviour;
	
	ParentNode parent;

	GUIMaintainDistanceTask view;
	
	AI_Movement moveComponent;
	
	GameObject opponent;
	
	float desiredSqrDistance = 9.0f; //at what distance do you want to stay?
	float sqrDesiredRange = 2.0f;	//whats the range around your desired Distance that you are okay with
	
	
	public MaintainDistanceTask(ParentNode parent, Behaviour rootBehaviour, GUIMaintainDistanceTask view)
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
	
	public void Activate()
	{
		Debug.Log ("maintainDisttask: activate");
		
		rootBehaviour.activateTask(this);
	}
	
	public void Deactivate()
	{
		Debug.Log("maintainDisttask: deactivate");
		moveComponent.stop();
		rootBehaviour.deactivateTask(this);
		
	}
	
	
	public void PerformTask()
	{
		float sqrDistToOpponent = (opponent.transform.position - moveComponent.transform.position).sqrMagnitude;

		//is the AI or the player falling off the board?
		if(!moveComponent.getOnFloor())
		{
			Debug.Log ("maintainDisttask fail: AI falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		else if(!opponent.GetComponent<PlayerMovement>().getOnFloor())
		{
			Debug.Log ("maintainDisttask fail: Player falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//are you at a good distance?
		else if( (sqrDistToOpponent > desiredSqrDistance - sqrDesiredRange) && (sqrDistToOpponent < desiredSqrDistance + sqrDesiredRange))
		{
			Debug.Log ("maintainDisttask succeed: good Distance");
			Deactivate();
			parent.ChildDone(this, true);
		}

		//are you too close?
		else if(sqrDistToOpponent < desiredSqrDistance - sqrDesiredRange)
		{
			moveComponent.rep_evadePosition(opponent.transform, desiredSqrDistance);
		}
		//are you too far away?
		else if(sqrDistToOpponent > desiredSqrDistance + sqrDesiredRange)
		{
			moveComponent.attr_SeekPosition(opponent.transform);
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
}
