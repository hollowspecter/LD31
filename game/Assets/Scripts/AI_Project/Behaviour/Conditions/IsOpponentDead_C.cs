using UnityEngine;
using System.Collections;

public class IsOpponentDead_C: ChildNode 
{
	
	ParentNode parent;
	
	Movement enemyMovement;
	
	public IsOpponentDead_C(ParentNode p, Movement m)
	{
		parent = p;
		parent.AddChild(this);
		enemyMovement = m;
	}
	
	public void Activate()
	{
		bool isEnemyDead = !(enemyMovement.getOnFloor());
		
		parent.ChildDone(this, isEnemyDead);
	}
	
}
