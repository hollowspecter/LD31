using UnityEngine;
using System.Collections;

public class SeekOpponentTask : TaskNode 
{
	ParentNode parent;

	AI_Movement moveComponent;
	Transform opponent_T;

	public SeekOpponentTask(ParentNode parent)
	{
		this.parent = parent;
	}

	public void Activate()
	{
		moveComponent = GameObject.FindGameObjectWithTag("Player 1").GetComponent<AI_Movement>();
		if(moveComponent == null)
		{
			moveComponent = GameObject.Find("AI_Deer").GetComponent<AI_Movement>();
		}

		opponent_T = GameObject.FindGameObjectWithTag("Player 0").transform;
		if(opponent_T == null)
		{
			opponent_T = GameObject.Find("boar").transform;
		}

		moveComponent.SetTarget(opponent_T);
	}
	
	public void PerformTask()
	{
		if(moveComponent.getHasArrived())
		{
			parent.ChildDone(this, true);
		}
		else if(!moveComponent.getOnFloor())
		{

		}
	}
}
