using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GUISequence : GUINode 
{
	Sequence model;

	public GUISequence(int value, Vector2 position) : base("Sequence", value, position)
	{
		baseColor = Color.yellow;
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

	public void SetModel(Sequence model)
	{
		this.model = model;
	}

	public override Node GetModel()
	{
		return model;
	}

	public override bool CanHaveMoreChildren ()
	{
		return true;
	}

	public override List<GUINode> GetAllChildren ()
	{
		List<GUINode> children = new List<GUINode>();
		
		foreach(ChildNode child in model.GetChildren())
		{	
			children.Add(child.GetView());
			foreach(GUINode n in child.GetView().GetAllChildren())
			{
				children.Add(n);
			}
		}
		
		return children;
	}

}