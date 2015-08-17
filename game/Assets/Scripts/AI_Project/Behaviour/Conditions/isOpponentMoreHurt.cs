using UnityEngine;
using System.Collections;

public class IsOpponentMoreHurt : ChildNode 
{
	
	ParentNode parent;
	
	PlayerStance ownStance;
	PlayerStance oppStance;
	
	float hurtValue = 33.0f;
	bool randomize = false;
	
	public IsOpponentMoreHurt(ParentNode p, bool randomize)
	{
		parent = p;
		parent.AddChild(this);
		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}
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
		Debug.Log ("is the Opponent more hurt?");
		float ownHurt = ownStance.currentMultiplier;
		float oppHurt = oppStance.currentMultiplier;

		if(randomize)
		{
			ownHurt += Random.Range(-ownHurt/3, ownHurt/3);
			oppHurt += Random.Range(-oppHurt/3, oppHurt/3);
		}
		bool playerMore = (oppHurt > ownHurt);
		if(playerMore)
			Debug.Log("Player hurt more than AI");

		parent.ChildDone(this, playerMore);
	}
	
	public void Deactivate()
	{
		
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
