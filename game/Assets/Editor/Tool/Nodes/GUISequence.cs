using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GUISequence : GUINode 
{
	Sequence model;

	public GUISequence(int value, Vector2 position) : base("Sequence", value, position)
	{
		//model = new Sequence();
	}

	public override void AddChild(ChildNode child)
	{
		Debug.Log ("add child to sequence");
	}

	public override void DrawChildLines()
	{
		
		/*value = children.Count;
		foreach(GUINode child in children)
		{
			if(child == null)
			{
				children.Remove(child);
			}
			Handles.BeginGUI();
			Handles.color = Color.black;
			Vector3 start = new Vector3(this.GetBotPosition().x, this.GetBotPosition().y);
			Vector3 end = new Vector3(child.GetTopPosition().x, child.GetTopPosition().y);
			Handles.DrawBezier(start,
			                   end,
			                   start + new Vector3(0 , 100),
			                   end + new Vector3(0, -100),
			                   Color.black,null, 4);
			Handles.EndGUI(); 
		}*/
	}
	
}