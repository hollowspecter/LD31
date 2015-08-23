using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUIFlankOpponentTask : GUITask
{
	FlankOpponentTask model;

	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUIFlankOpponentTask(int value, Vector2 position) : base("Flank Opponent!", value, position, 12)
	{

	}

	public void SetModel(FlankOpponentTask model)
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
