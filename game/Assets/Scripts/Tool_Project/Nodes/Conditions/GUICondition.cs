using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class GUICondition : GUINode 
{	
	Vector2 offsetToParent;
	
	public GUICondition(string typeName, int value, Vector2 position, int type) : base(typeName, value, position)
	{
		baseColor = Color.green;
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
	
	public override void DrawParentLine()
	{
		if(GetModel() is ChildNode)
		{
			GUINode parent = ((ChildNode)GetModel()).GetParent().GetView();
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
		if(!selected && (GetModel() is ChildNode))
			position = ((ChildNode)GetModel()).GetParent().GetView().Position - offsetToParent;
		base.Update();
	}
	
	public override void DragUpdate()
	{
		if(GetModel() is ChildNode)
			offsetToParent = ((ChildNode)GetModel()).GetParent().GetView().Position - position;
	}
	
	public override void DragEnd ()
	{
		if(GetModel() is ChildNode)
			((ChildNode)GetModel()).GetParent().ChildEvent((ChildNode)GetModel());
	}
	
}
