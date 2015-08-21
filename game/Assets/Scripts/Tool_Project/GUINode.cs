using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GUINode : DraggableGUIElement
{
	protected string name;
	protected int NodeID;
	protected int TypeID;
	protected Color baseColor;

	int width = 80;
	int height = 50;

	Vector2 topPosition;
	Vector2 botPosition;

	Rect drawRect;
	Rect selectRect;
	Rect topRect;
	Rect botRect;

	protected bool selected;

	public GUINode(string name, int value, Vector2 position) : base(position)
	{
		this.name = name;
		this.NodeID = value;
		TypeID = -1;
		topPosition = new Vector2(position.x, position.y - height/2 - height/4);
		botPosition = new Vector2(position.x, position.y + height/2 + height/4);

		baseColor = Color.white;
	}
		
	public void OnGUI()
	{
		//Debug.Log ("node:onGUI");
		drawRect = new Rect(position.x - width/2, position.y - height/2, width, height);
		selectRect = new Rect(position.x - (width/2 + 3), position.y - (height/2 +3), width + 6, height + 6);

		topRect = new Rect(topPosition.x - width/8, topPosition.y, width/4, height/4);
		botRect = new Rect(botPosition.x - width/8, botPosition.y-height/4 ,width/4, height/4);

		if(selected)
			Drag(drawRect);

		DrawMainRect();
		DrawParentConnector();
		DrawChildConnector();
		
		DrawParentLine();

		Update();


		GUI.color = Color.white;
	}

	public virtual void Update()
	{
		topPosition = new Vector2(position.x, position.y - height/2 - height/4);
		botPosition = new Vector2(position.x, position.y + height/2 + height/4);
	}

	public virtual void DrawMainRect()
	{

		if(selected)
		{
			GUI.color = Color.red;
			GUI.Box(selectRect, "");
		}
		GUI.color = baseColor;
		GUILayout.BeginArea(drawRect, GUI.skin.GetStyle("Box"));
		GUILayout.Label(name);
		GUILayout.Label(NodeID.ToString());
		GUILayout.EndArea();
	}

	public virtual void DrawParentConnector()
	{
		GUI.color = Color.green;
		GUILayout.BeginArea(topRect, GUI.skin.GetStyle("Box"));
		GUILayout.EndArea();
	}

	public virtual void DrawChildConnector()
	{
		GUI.color = Color.blue;
		GUILayout.BeginArea(botRect, GUI.skin.GetStyle("Box"));
		GUILayout.EndArea();
	}

	public virtual void DrawParentLine()
	{

	}
	public virtual bool CanHaveMoreChildren()
	{
		Debug.Log("virtual call");
		return false;
	}

	public virtual List<GUINode> GetAllChildren()
	{
		Debug.Log ("virtual children");
		return null;
	}


	/******GETTER & SETTER*************/

	public virtual Node GetModel()
	{
		Debug.Log ("this NodeType has no model(virtual call)");
		return null;
	}

	public Vector2 GetTopPosition()
	{
		return topPosition;
	}

	public Vector2 GetBotPosition()
	{
		return botPosition;
	}

	public Rect getMainRect()
	{
		return drawRect;
	}

	public void SetNodeID(int i)
	{
		NodeID = i;
	}

	public int GetNodeID()
	{
		return NodeID;
	}

	public int GetTypeID()
	{
		return TypeID;
	}

	public bool Select
	{
		get
		{
			return selected;
		}
		set
		{
			selected = value;
		}
	}


}
