using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUIEvadeOpponentTask : GUITask 
{
	EvadeOpponentTask model;

	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUIEvadeOpponentTask(int value, Vector2 position) : base("Evade Opponent!", value, position, 11)
	{

	}

	public void SetModel(EvadeOpponentTask model)
	{
		this.model = model;
		DragUpdate();
		DragEnd();
	}
	
	public override Node GetModel()
	{
		return model;
	}

}
