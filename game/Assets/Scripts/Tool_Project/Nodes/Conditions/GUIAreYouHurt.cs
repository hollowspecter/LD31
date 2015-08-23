using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUIAreYouHurt : GUICondition 
{
	AreYouHurt model;
	
	Vector2 offsetToParent;
	
	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUIAreYouHurt(int value, Vector2 position) : base("Are You Hurt?", value, position, 15)
	{

	}
	
	public void SetModel(AreYouHurt model)
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
