using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUISeekNearestHealthTask : GUITask
{
	SeekNearestHealthTask model;

	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUISeekNearestHealthTask(int value, Vector2 position) : base("Seek Health!", value, position, 14)
	{

	}

	public void SetModel(SeekNearestHealthTask model)
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
