using UnityEngine;
using System.Collections;

public class AreYouHurt_C: ChildNode 
{

	ParentNode parent;

	PlayerStance ownStance;

	float hurtValue = 20.0f;

	public AreYouHurt_C(ParentNode p)
	{
		parent = p;
		parent.AddChild(this);
		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}
		ownStance = self.GetComponent<PlayerStance>();

	}

	public void Activate()
	{
		Debug.Log ("are you hurt?");
		bool areYouHurt = (ownStance.currentMultiplier > hurtValue);
		if(areYouHurt)
			Debug.Log("AI hurt over " + hurtValue + "%");
		
		parent.ChildDone(this, areYouHurt);
	}

	public void Deactivate()
	{

	}
	
}
