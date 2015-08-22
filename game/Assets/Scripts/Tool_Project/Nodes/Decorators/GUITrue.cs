using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GUITrue : GUINode 
{
	True model;

	Vector2 offsetToParent;
	
	public GUITrue(int value, Vector2 position) : base("True", value, position)
	{
		baseColor = Color.yellow;
		TypeID = 3;
	}
	public void SetModel(True model)
	{
		this.model = model;
		DragUpdate();
		DragEnd ();
	}
	
	public override bool CanHaveMoreChildren ()
	{
		return (model.GetChild() == null);
	}
	
	public override List<GUINode> GetAllChildren ()
	{
		List<GUINode> children = new List<GUINode>();
		
		if(model.GetChild() != null)
		{
			children.Add(model.GetChild().GetView());
			foreach(GUINode n in model.GetChild().GetView().GetAllChildren())
			{
				children.Add(n);
			}	
		}
		
		return children;
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

	public override void DragEnd ()
	{
		model.GetParent().ChildEvent(model);
	}
	
	public override Node GetModel()
	{
		return model;
	}
}
