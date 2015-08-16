using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BehaviourWindow : EditorWindow
{	
	//[MenuItem("Window/Test Editor")]

	BehaviourTree behaviour;

	float currentToolWidth;
	float minToolWidth = 285;
	float maxToolWidth;
	Rect cursorChangeRect;
	Rect mainAreaRect;
	Rect deleteRect;

	List<GUINode> guiNodes;
	GUINode selected;

	bool resize = false;

	public void Init () 
	{
		// Get existing open window or if none, make a new one:
		behaviour = GameObject.FindGameObjectWithTag("Player1").GetComponent<BehaviourTree>();
		BehaviourWindow window = (BehaviourWindow)EditorWindow.GetWindow<BehaviourWindow>("Behaviour Tree Editor");
		window.Show();
	}

	public void OnEnable()
	{
		BehaviourWindow window = (BehaviourWindow)EditorWindow.GetWindow (typeof (BehaviourWindow));
		window.titleContent.text = "Behaviour";
		window.titleContent.tooltip = "Here you can edit your Behaviour Tree";
		currentToolWidth = this.position.width/3;
		maxToolWidth = this.position.width - minToolWidth;
		cursorChangeRect = new Rect(currentToolWidth-5, 20, 10,this.position.height);
		mainAreaRect = new Rect(currentToolWidth, 20, position.width-currentToolWidth, position.height);
		deleteRect = new Rect(currentToolWidth - 150, position.height - 200, 145, 170);
		guiNodes = new List<GUINode>();
		selected = null;
	}

	void OnGUI()
	{	
		GUINode delete;
		
		bool wasDragging;
		Color color;

		GUILayout.Toolbar(-1, new string[]{"Tool0", "Tool1", "Tool2", "Tool3", "Tool4", "Tool5"});

		//left Toolbar
		GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

			GUILayout.BeginVertical("box", GUILayout.Width(currentToolWidth), GUILayout.ExpandHeight(true));
				//Debug.Log (currentToolWidth);
				behaviour.color = EditorGUILayout.ColorField(behaviour.color, GUILayout.Width(200));

				GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
				behaviour.myString = EditorGUILayout.TextField ("Text Field", behaviour.myString);
				
				behaviour.groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", behaviour.groupEnabled);
				behaviour.myBool = EditorGUILayout.Toggle ("Toggle", behaviour.myBool);
				behaviour.myFloat = EditorGUILayout.Slider ("Slider", behaviour.myFloat, -3, 3);
				EditorGUILayout.EndToggleGroup ();


				if(GUILayout.Button("New Selector"))
				{
					GUINode tmp = new GUISelector(guiNodes.Count, mainAreaRect.center);
					guiNodes.Add(tmp);
					if(selected != null)
						//selected.AddChild(tmp);
					SelectNode(guiNodes[guiNodes.Count-1]);
				}
				if(GUILayout.Button("New Sequence"))
				{
					GUINode tmp = new GUISequence(guiNodes.Count, mainAreaRect.center);
					guiNodes.Add(tmp);
					if(selected != null)
						//selected.AddChild(tmp);
					SelectNode(guiNodes[guiNodes.Count-1]);
				}

				GUILayout.Button("Button3");
			GUILayout.EndVertical();

			GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
				GUI.color = Color.red;
				GUILayout.BeginArea(deleteRect, GUI.skin.GetStyle("Box"));
					GUILayout.Label("delete");
				GUILayout.EndArea();
				GUI.color = Color.white;
			GUILayout.EndVertical();

		GUILayout.EndHorizontal();
		
		delete = null;

		if(guiNodes.Count > 0)
		{
			for(int i = 0; i < guiNodes.Count; ++i)
			{
				GUINode node = guiNodes[i];

				wasDragging = node.IsDragging;

				node.OnGUI();

				if(i+1 < guiNodes.Count)
				{/*
					Handles.BeginGUI();
					Handles.color = Color.black;
					Vector3 start = new Vector3(node.GetBotPosition().x, node.GetBotPosition().y);
					Vector3 end = new Vector3(guiNodes[i+1].GetTopPosition().x, guiNodes[i+1].GetTopPosition().y);
					Handles.DrawBezier(start,
					             		end,
					                   	start + new Vector3(0 , 100),
						                end + new Vector3(0, -100),
						               	Color.black,null, 4);
					Handles.EndGUI();  */          
				}

				if(node.IsDragging)
				{
					if(!wasDragging)
						SelectNode(node);
				}

				else if(wasDragging)
				{
					if(deleteRect.Contains(Event.current.mousePosition))
					{
						delete = node;
					}
				}
				else if(Event.current.type == EventType.MouseUp && node.IsMouseHover(node.getMainRect()))
				{
					Debug.Log ("select");
					SelectNode(node);
				}

			}
		}
		
		if(delete != null)
		{

			guiNodes.Remove(delete);
			DeselectNode();
		}


		ResizeBar();
		Repaint();

	}

	void ResizeBar()
	{		
		maxToolWidth = this.position.width - minToolWidth;
		//GUI.DrawTexture(cursorChangeRect,EditorGUIUtility.whiteTexture);
		EditorGUIUtility.AddCursorRect(cursorChangeRect,MouseCursor.ResizeHorizontal);

		if( Event.current.type == EventType.mouseDown && cursorChangeRect.Contains(Event.current.mousePosition))
		{
			resize = true;
		}

		if(resize)
		{
			float tmpX = Event.current.mousePosition.x;
			currentToolWidth = tmpX;
			if(tmpX < minToolWidth)
				currentToolWidth = minToolWidth;
			else if(tmpX > maxToolWidth)
				currentToolWidth = maxToolWidth;

			cursorChangeRect.Set(currentToolWidth-5, 20,10,this.position.height);
			deleteRect = new Rect(30, position.height -100, currentToolWidth-10,90);
		}
		if(Event.current.type == EventType.MouseUp)
			resize = false;
	}

	void SelectNode(GUINode node)
	{
		if(selected != null)
			selected.Select = false;
		selected = node;
		node.Select = true;
	}

	void DeselectNode()
	{
		selected.Select = false;
		selected = null;
	}

	void DeleteNode(GUINode node)
	{

	}

}
