using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BehaviourWindow : EditorWindow
{	
	//[MenuItem("Window/Test Editor")]

	BehaviourTree behaviourTree;

	float currentToolWidth;
	float minToolWidth = 285;
	float maxToolWidth;
	Rect cursorChangeRect;

	Rect mainAreaRect;

	Rect deleteRect;

	List<GUINode> guiNodes;
	GUINode selected;

	bool resize = false;

	Vector2 scrollPos;

	public void Init () 
	{
		// Get existing open window or if none, make a new one:
		behaviourTree = GameObject.FindGameObjectWithTag("Player1").GetComponent<BehaviourTree>();
		behaviourTree.Init();
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
		deleteRect = new Rect(7, mainAreaRect.height - 200, 100, 170);
		guiNodes = new List<GUINode>();
		selected = null;

		scrollPos = Vector2.zero;
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
				behaviourTree.color = EditorGUILayout.ColorField(behaviourTree.color, GUILayout.Width(200));

				GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
				behaviourTree.myString = EditorGUILayout.TextField ("Text Field", behaviourTree.myString);
				
				behaviourTree.groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", behaviourTree.groupEnabled);
				behaviourTree.myBool = EditorGUILayout.Toggle ("Toggle", behaviourTree.myBool);
				behaviourTree.myFloat = EditorGUILayout.Slider ("Slider", behaviourTree.myFloat, -3, 3);
				EditorGUILayout.EndToggleGroup ();
				
				EditorGUI.BeginDisabledGroup(guiNodes.Count > 0);
				if(GUILayout.Button("New Behaviour"))
		 		{
					GUIBehaviour tmp = new GUIBehaviour(guiNodes.Count,scrollPos + mainAreaRect.center + new Vector2 ( 0, -mainAreaRect.height/2)- new Vector2(currentToolWidth, 0));
					guiNodes.Add(tmp);
					behaviourTree.SetBaseNode(new Behaviour(tmp, behaviourTree));
					SelectNode(guiNodes[guiNodes.Count-1]);
				}
				EditorGUI.EndDisabledGroup();
				EditorGUI.BeginDisabledGroup(selected == null);
					if(GUILayout.Button("New Selector"))
					{
						GUISelector tmp = new GUISelector(guiNodes.Count, scrollPos + mainAreaRect.center - new Vector2(currentToolWidth, 0));
						guiNodes.Add(tmp);
						Selector s = new Selector((ParentNode)selected.GetModel(), tmp);
						behaviourTree.AddNode(s);
						SelectNode(guiNodes[guiNodes.Count-1]);
					}
					if(GUILayout.Button("New Sequence"))
					{
						GUISequence tmp = new GUISequence(guiNodes.Count, scrollPos + mainAreaRect.center - new Vector2(currentToolWidth, 0));
						guiNodes.Add(tmp);
			Sequence s = new Sequence((ParentNode)selected.GetModel(), tmp);
						behaviourTree.AddNode(s);
						SelectNode(guiNodes[guiNodes.Count-1]);
					}
				EditorGUI.EndDisabledGroup();
				
				GUILayout.Space(280);
				GUI.color = Color.red;
				if(GUILayout.Button("Delete All"))
				{
					DeselectNode();
					foreach(GUINode node in guiNodes)
					{
						DeleteNode(node);
					}
				}
				
				GUI.color = Color.white;
			GUILayout.EndVertical();
			scrollPos = GUI.BeginScrollView(mainAreaRect, scrollPos, new Rect(0,0, 10000, 2000),true, true);
		
				GUI.color = Color.red;
				GUILayout.BeginArea(deleteRect, GUI.skin.GetStyle("Box"));
					GUILayout.Label("delete");
				GUILayout.EndArea();
				GUI.color = Color.white;

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
							if(!wasDragging && selected == null)
								SelectNode(node);
						}
						
						else if(wasDragging)
						{
							if(deleteRect.Contains(Event.current.mousePosition))
							{
								Debug.Log ("contain" + node);
								delete = node;
							}
						}
						else if(Event.current.type == EventType.MouseDown && node.IsMouseHover(node.getMainRect()))
						{
							//Debug.Log ("select");
							SelectNode(node);
						}
						
					}
				}
		

			GUI.EndScrollView();

		GUILayout.EndHorizontal();
		
		if(delete != null)
		{
			Debug.Log ("delete");
			DeselectNode();
			DeleteNode(delete);
		}
		//GUI.DrawTexture(mainAreaRect, EditorGUIUtility.whiteTexture);
		mainAreaRect = new Rect(currentToolWidth, 20, position.width-currentToolWidth, position.height);
		deleteRect = new Rect(scrollPos.x + 7, scrollPos.y +  mainAreaRect.height - 200, 100, 170);
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
		node.GetModel().Delete();
		guiNodes.Remove(node);
	}

}
