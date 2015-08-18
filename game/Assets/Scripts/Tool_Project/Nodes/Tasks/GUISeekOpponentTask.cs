using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public class GUISeekOpponentTask : GUINode 
{
	SeekOpponentTask model;

	public GUISeekOpponentTask(int value, Vector2 position) : base("SeekTask", value, position)
	{
		baseColor = Color.blue;
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

	public override void DrawChildConnector ()
	{

	}

	public override Node GetModel()
	{
		return model;
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
}
