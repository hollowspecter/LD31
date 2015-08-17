using UnityEngine;
using System.Collections;

public class AttackTask : TaskNode 
{
	Behaviour rootBehaviour;
	
	ParentNode parent;

	AI_Movement moveComponent;
	HitReach hitReach;
	PlayerJab playerJab;
	bool isAttacking;
	float lastAttackTime;
	
	public AttackTask(ParentNode parent, Behaviour rootBehaviour)
	{
		this.parent = parent;
		this.parent.AddChild(this);
		this.rootBehaviour = rootBehaviour;
		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}
		hitReach = self.GetComponentInChildren<HitReach>();
		playerJab = self.GetComponent<PlayerJab>();
		moveComponent = self.GetComponent<AI_Movement>();
	}
	
	public void Activate()
	{
		rootBehaviour.activateTask(this);
		isAttacking = false;
	}
	
	public void Deactivate()
	{
		rootBehaviour.deactivateTask(this);
	}
	
	public void PerformTask()
	{
		//has the target left your reach?
		if(!hitReach.opposingPlayerInReach())
		{
			Debug.Log ("attacktask fail: out of reach");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//is the AI falling off the board?
		else if(!moveComponent.getOnFloor())
		{
			Debug.Log ("attacktask fail: AI falls");
			Deactivate();
			parent.ChildDone(this, false);
		}
		//if neither: try to attack the opponent

		else if(!isAttacking)
		{
			playerJab.getAnim().SetTrigger("Jab");
			if(Random.value > 0.5)
			{
				playerJab.getAnim().SetTrigger("Combo");
			}
			if(Random.value > 0.75)
			{
				playerJab.getAnim().SetTrigger("Kick");
			}
			isAttacking = true;
		}
		AnimatorStateInfo info = playerJab.getAnim().GetCurrentAnimatorStateInfo(1);
		bool isJabbing = info.IsName("jabR") || info.IsName("jabL");
		bool isComboing = info.IsName ("comboR") || info.IsName ("comboL");
		bool isKicking = info.IsName ("kickR") || info.IsName ("kickL");
		if(isAttacking && !(isJabbing || isComboing || isKicking) && Time.time > (lastAttackTime + 1.5f))
		{
			Debug.Log ("attacktask suceed: have Attacked");
			Deactivate();
			isAttacking = false;
			parent.ChildDone(this, true);
			lastAttackTime = Time.time;
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
