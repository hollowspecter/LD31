using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUISeekOpponentTask : GUITask 
{
	SeekOpponentTask model;
	
	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUISeekOpponentTask(int value, Vector2 position) : base("Seek Opponent!", value, position, 10)
	{

	}

	public void SetModel(SeekOpponentTask model)
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
