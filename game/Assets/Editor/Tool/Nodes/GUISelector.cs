using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUISelector : GUINode 
{
	Selector model;

	public GUISelector(int value, Vector2 position) : base("Selector", value, position)
	{

	}

	public override void AddChild(ChildNode child)
	{
		model.AddChild(child);
	}

	public override void SetParent(ParentNode parent)
	{

	}



}
