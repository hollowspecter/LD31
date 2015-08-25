using UnityEngine;
using System.Collections;

public class IsOpponentHurt: ChildNode
{
	
	ParentNode parent;
	
	PlayerStance oppStance;
	
	float hurtValue = 33.0f;
	bool randomize = false;
	
	public IsOpponentHurt(ParentNode p, bool randomize)
	{
		parent = p;
		parent.AddChild(this);
		GameObject opponent = GameObject.FindGameObjectWithTag("Player0");
		if(opponent == null)
		{
			opponent = GameObject.Find("boar");
		}
		
		oppStance = opponent.GetComponent<PlayerStance>();
		
		this.randomize = randomize;
	}
	
	public void Activate()
	{
		Debug.Log ("is the opponent hurt?");
		float tmpHurt = hurtValue;
		if(randomize)
		{
			tmpHurt += Random.Range(-hurtValue/3, hurtValue/3);
		}
		
		bool isOpponentHurt = (oppStance.currentMultiplier > tmpHurt);
		if(isOpponentHurt)
			Debug.Log("Opponent hurt over " + hurtValue + "%");
		
		parent.ChildDone(this, isOpponentHurt);
	}
	
	public void Deactivate()
	{
		
	}
	
}
