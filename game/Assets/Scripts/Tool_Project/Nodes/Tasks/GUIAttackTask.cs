using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUIAttackTask : GUITask 
{
	AttackTask model;

	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUIAttackTask(int value, Vector2 position) : base("Attack!", value, position, 9)
	{

	}
	
	public void SetModel(AttackTask model)
	{
		this.model = model;
		DragUpdate();
		DragEnd ();
	}

	public override Node GetModel()
	{
		return model;
	}
}
