using UnityEngine;
using System.Collections;

public class IsOpponentDead_C: ChildNode 
{
	
	ParentNode parent;
	
	Movement enemyMovement;
	
	public IsOpponentDead_C(ParentNode p)
	{
		parent = p;
		parent.AddChild(this);
		GameObject opponent = GameObject.FindGameObjectWithTag("Player0");
		if(opponent == null)
		{
			opponent = GameObject.Find("boar");
		}
		enemyMovement = opponent.GetComponent<PlayerMovement>();
	}
	
	public void Activate()
	{
		bool isEnemyDead = enemyMovement.getIsFalling();
		
		parent.ChildDone(this, isEnemyDead);
	}

	public void Deactivate()
	{
		
	}
	
}
