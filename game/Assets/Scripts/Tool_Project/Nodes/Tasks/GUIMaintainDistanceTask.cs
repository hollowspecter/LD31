using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUIMaintainDistanceTask : GUITask
{
	MaintainDistanceTask model;

	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUIMaintainDistanceTask(int value, Vector2 position) : base("Maintain Distance!", value, position, 13)
	{
	}
	
	public void SetModel(MaintainDistanceTask model)
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
