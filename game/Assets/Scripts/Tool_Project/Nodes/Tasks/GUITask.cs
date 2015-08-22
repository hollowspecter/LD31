using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUITask : GUINode 
{
	TaskNode model;
	
	Vector2 offsetToParent;
	
	public GUITask(int value, Vector2 position, int type, string typeName) : base(typeName, value, position)
	{
		baseColor = new Color(0.5f, 0.6f, 1.0f);
		TypeID = type;
	}
	
	public override void DrawChildConnector ()
	{
		
	}
	
	public override bool CanHaveMoreChildren ()
	{
		return false;
	}
	
	public override List<GUINode> GetAllChildren ()
	{
		List<GUINode> children = new List<GUINode>();	
		return children;
	}
	
	public void SetModel(TaskNode model)
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
		Debug.Log (model.GetParent().GetType());
		if(!selected)
			//position = model.GetParent().GetView().Position - offsetToParent;
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
