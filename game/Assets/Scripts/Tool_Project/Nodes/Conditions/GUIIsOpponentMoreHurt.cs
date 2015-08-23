using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUIIsOpponentMoreHurt : GUICondition 
{
	IsOpponentMoreHurt model;
	
	Vector2 offsetToParent;

	//Define your Node Display Name and typeID by passing it to the base constructor
	public GUIIsOpponentMoreHurt(int value, Vector2 position) : base("Is Opponent More Hurt?", value, position, 16)
	{
		
	}
	
	public void SetModel(IsOpponentMoreHurt model)
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
