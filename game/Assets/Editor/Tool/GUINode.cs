using UnityEngine;
using UnityEditor;
using System.Collections;

public class GUINode : DraggableGUIElement
{
	protected string name;
	protected int value;

	int width = 80;
	int height = 40;

	Vector2 topPosition;
	Vector2 botPosition;

	Rect drawRect;
	Rect topRect;
	Rect botRect;

	bool selected;

	public GUINode(string name, int value, Vector2 position) : base(position)
	{
		this.name = name;
		this.value = value;
		topPosition = new Vector2(position.x, position.y - height/2 - height/4);
		botPosition = new Vector2(position.x, position.y + height/2 + height/4);
	}
		
	public void OnGUI()
	{
		//Debug.Log ("node:onGUI");
		drawRect = new Rect(position.x - width/2, position.y - height/2, width, height);

		topRect = new Rect(topPosition.x - width/8, topPosition.y, width/4, height/4);
		botRect = new Rect(botPosition.x - width/8, botPosition.y-height/4 ,width/4, height/4);


		color = selected ? Color.red : Color.white;
		
		Drag (drawRect);
		GUI.color = color;
		GUILayout.BeginArea(drawRect, GUI.skin.GetStyle("Box"));
			GUILayout.Label(name);
			GUILayout.Label(value.ToString());
			//dragRect = GUILayoutUtility.GetLastRect();
		GUILayout.EndArea();
		GUI.color = Color.green;
		GUILayout.BeginArea(topRect, GUI.skin.GetStyle("Box"));
		GUILayout.EndArea();

		GUI.color = Color.blue;
		GUILayout.BeginArea(botRect, GUI.skin.GetStyle("Box"));
		GUILayout.EndArea();
		
		//GUI.DrawTexture(dragRect, EditorGUIUtility.whiteTexture);


		topPosition = new Vector2(position.x, position.y - height/2 - height/4);
		botPosition = new Vector2(position.x, position.y + height/2 + height/4);

		DrawChildLines();
	}

	//This method is overridden by the inheriting classes
	//in order to connect Lines to their children
	public virtual void DrawChildLines()
	{

	}

	public virtual void AddChild(ChildNode child)
	{
		Debug.Log ("virtual addchild");
	}

	public virtual void SetParent(ParentNode parent)
	{
		Debug.Log ("virtual setparent");
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
