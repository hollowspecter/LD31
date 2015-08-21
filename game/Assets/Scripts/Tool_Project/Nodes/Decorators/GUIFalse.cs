using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GUIFalse : GUINode 
{
	False model;

	public GUIFalse(int value, Vector2 position) : base("False", value, position)
	{
		baseColor = Color.yellow;
		TypeID = 3;
	}
	
	public override void DrawParentLine()
	{
		if(model != null)
		{
			GUINode parent = model.GetParent().GetView();
			Handles.BeginGUI();
			Handles.color = Color.black;
			Vector3 start = new Vector3(parent.GetBotPosition().x, parent.GetBotPosition().y);
			Vector3 end = new Vector3(this.GetTopPosition().x, this.GetTopPosition().y);
			Handles.DrawBezier(start,
			                   end,
			                   start + new Vector3(0 , 50),
			                   end + new Vector3(0, -50),
			                   Color.black,null, 4);
			Handles.EndGUI();
		}
	}
	
	public void SetModel(False model)
	{
		this.model = model;
	}
	
	public override Node GetModel()
	{
		return model;
	}
	
	public override bool CanHaveMoreChildren ()
	{
		return (model.GetChild() == null);
	}
	
	public override List<GUINode> GetAllChildren ()
	{
		List<GUINode> children = new List<GUINode>();
			
		children.Add(model.GetChild().GetView());
		foreach(GUINode n in model.GetChild().GetView().GetAllChildren())
		{
			children.Add(n);
		}
		
		return children;
	}
}
