using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GUISequence : GUINode 
{
	Sequence model;

	Vector2 offsetToParent;

	public GUISequence(int value, Vector2 position) : base("Sequence", value, position)
	{
		baseColor = Color.yellow;
		TypeID = 2;
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

	public void SetModel(Sequence model)
	{
		this.model = model;
		DragUpdate();
	}


	//CAN ALWAYS COPY PASTE LIKE THIS
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

	public override void Update ()
	{
		if(!selected)
			position = model.GetParent().GetView().Position - offsetToParent;
		base.Update();
	}
	
	public override void DragUpdate()
	{
		offsetToParent = model.GetParent().GetView().Position - position;
	}
	
	public override Node GetModel()
	{
		return model;
	}
}