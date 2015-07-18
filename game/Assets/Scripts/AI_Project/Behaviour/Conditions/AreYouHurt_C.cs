using UnityEngine;
using System.Collections;

public class AreYouHurt_C: ChildNode 
{

	ParentNode parent;

	PlayerStance ownStance;

	public AreYouHurt_C(ParentNode p, PlayerStance ps)
	{
		parent = p;
		parent.AddChild(this);
		ownStance = ps;
	}

	public void Activate()
	{
		bool areYouHurt = (ownStance.currentMultiplier > 0.33f);
		
		parent.ChildDone(this, areYouHurt);
	}
	
}
