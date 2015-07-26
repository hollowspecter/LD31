using UnityEngine;
using System.Collections;


public class SeekNearestHealthTask: TaskNode 
{
	Behaviour rootBehaviour;
	
	ParentNode parent;
	
	AI_Movement moveComponent;
	GameObject[] healthPickup;
	GameObject nearestPickup;

	
	public SeekNearestHealthTask(ParentNode parent, Behaviour rootBehaviour)
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
		healthPickup = null;
	}
	
	public void Activate()
	{
		Debug.Log ("healthtask activate");

		if(healthPickup == null)
			healthPickup = GameObject.FindGameObjectsWithTag("HealthPickup");
		
		nearestPickup = getNearestHealthPickup();

		rootBehaviour.activateTask(this);
		if(nearestPickup != null)
			moveComponent.SetTarget(nearestPickup.transform);
		moveComponent.setSeeking(true);
		moveComponent.setstopDist(1.0f);
	}
	
	public void Deactivate()
	{
		healthPickup = null;
		nearestPickup = null;
		moveComponent.SetTarget(null);
		moveComponent.setSeeking(false);
		moveComponent.setstopDist(9.0f);
		moveComponent.stop();
		rootBehaviour.deactivateTask(this);
		
	}

	public void PerformTask()
	{
		//has the AI arrived at its target?
		if(moveComponent.getHasArrived())
		{
			Debug.Log ("healthtask succeed: arrived");
			Deactivate();
			parent.ChildDone(this, true);
		}
		//are there any pickups?
		else if(healthPickup.Length < 1)
		{
			Debug.Log ("healthtask fail: no Pickup");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//did the player take the pickup?
		else if(nearestPickup == null)
		{
			Debug.Log ("healthtask fail: pickup taken");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//is the AI or the player falling off the board?
		else if(!moveComponent.getOnFloor() )
		{
			Debug.Log ("seektask failp: AI falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//if neither: seek the target through subtargets
		else
		{
			moveComponent.attr_SeekSubtarget();
		}
	}

	//compare all Healthlocations to find the nearest available health pickup
	GameObject getNearestHealthPickup()
	{
		float minDistance = float.MaxValue;
		GameObject nearestPickup = null;

		foreach(GameObject o in healthPickup)
		{
			float distance = (o.transform.position - moveComponent.transform.position).sqrMagnitude;

			if(distance < minDistance)
			{
				minDistance = distance;
				nearestPickup = o;
			}
		}

		return nearestPickup;
	}
	
}
