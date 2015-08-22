using UnityEngine;
using System.Collections;

public class AreYouHurt: ChildNode
{

	ParentNode parent;

	GUIAreYouHurt view;

	PlayerStance ownStance;

	float hurtValue = 33.0f;
	bool randomize = false;

	public AreYouHurt(ParentNode p, bool randomize, GUIAreYouHurt view)
	{
		parent = p;
		parent.AddChild(this);

		this.view = view;
		this.view.SetModel(this);

		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}
		ownStance = self.GetComponent<PlayerStance>();
		
		this.randomize = randomize;
	}

	public void Activate()
	{
		Debug.Log ("are you hurt?");
		float tmpHurt = hurtValue;
		if(randomize)
		{
			tmpHurt += Random.Range(-hurtValue/3, hurtValue/3);
		}

		bool areYouHurt = (ownStance.currentMultiplier > tmpHurt);
		if(areYouHurt)
			Debug.Log("AI hurt over " + hurtValue + "%");
		
		parent.ChildDone(this, areYouHurt);
	}

	public void Deactivate()
	{

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
