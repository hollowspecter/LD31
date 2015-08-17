using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIBehaviour : GUINode 
{
	Behaviour model;
	
	public GUIBehaviour(int value, Vector2 position) : base("Behaviour", value, position)
	{
		//model = new Sequence();
		baseColor = Color.cyan;
	}

	public override void DrawParentConnector()
	{
		//no parent
	}
	
	public override void DrawParentLine()
	{
		//no parent
	}

	public void SetModel(Behaviour model)
	{
		this.model = model;
	}

	public override Node GetModel()
	{
		return model;
	}
	
}