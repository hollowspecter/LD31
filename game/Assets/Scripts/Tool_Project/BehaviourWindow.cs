using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BehaviourWindow : EditorWindow
{	
	//[MenuItem("Window/Test Editor")]

	public GUISkin skin;

	BehaviourTree behaviourTree;

	float currentToolWidth;
	float minToolWidth = 285;
	float maxToolWidth;
	Rect cursorChangeRect;

	string lastTreePath;

	Rect mainAreaRect;

	Rect deleteRect;

	GUINode selected;

	bool resize = false;

	Vector2 scrollPos;
	Vector2 menuScrollPos;

	bool showDecorators = false;
	bool showTasks = false;
	bool showConditions = false;

	bool saved = false;

	public void Init () 
	{
		// Get existing open window or if none, make a new one:
		behaviourTree = GameObject.FindGameObjectWithTag("Player1").GetComponent<BehaviourTree>();
		behaviourTree.Init();

		BehaviourWindow window = (BehaviourWindow)EditorWindow.GetWindow<BehaviourWindow>("Behaviour Tree Editor");
		window.minSize = new Vector2(600.0f, 300.0f);
		window.wantsMouseMove = true;
		window.Show();
		//EditorWindow.FocusWindowIfItsOpen();

	}

	public void OnEnable()
	{
		if(behaviourTree == null)
		{
			behaviourTree = GameObject.FindGameObjectWithTag("Player1").GetComponent<BehaviourTree>();
		}
		behaviourTree.Init();
		BehaviourWindow window = (BehaviourWindow)EditorWindow.GetWindow (typeof (BehaviourWindow));
		window.titleContent.text = "Behaviour";
		window.titleContent.tooltip = "Here you can edit your Behaviour Tree";
		currentToolWidth = minToolWidth;
		maxToolWidth = this.position.width - minToolWidth;
		cursorChangeRect = new Rect(currentToolWidth - 5, 20, 10,this.position.height);

		mainAreaRect = new Rect(currentToolWidth + 10, 0, position.width-currentToolWidth - 10, position.height);
		deleteRect = new Rect(7, mainAreaRect.height - 170, 130, 150);
		selected = null;

		scrollPos = Vector2.zero;

		saved = true;

		if(!(behaviourTree.GetCurrentFilePath() == ""))
		{
			behaviourTree.GetFactory().ReadFromPath(behaviourTree.GetCurrentFilePath());
		}
	}

	void OnGUI()
	{
		if(skin != null)
			GUI.skin = skin;

		MoveAround();
		ResizeBar();

		DrawGraphArea();
		DrawMenuArea();

		mainAreaRect = new Rect(currentToolWidth + 10, 0, position.width-currentToolWidth -10, position.height);
		deleteRect = new Rect(scrollPos.x + 7, scrollPos.y +  mainAreaRect.height - 170, 130, 150);

		Repaint();

	}


	void DrawGraphArea()
	{
		scrollPos = GUI.BeginScrollView(mainAreaRect, scrollPos, new Rect(0,0, 4000, 4000));
		GUINode delete;
		bool wasDragging;
		Color color;


		
		GUI.color = Color.red;
		Texture2D bin = EditorGUIUtility.Load("Bin-512.png") as Texture2D;
		GUILayout.BeginArea(deleteRect, GUI.skin.GetStyle("Box"));
			GUILayout.Label(bin);
			GUILayout.Label("delete");
		GUILayout.EndArea();
		GUI.color = Color.white;
		
		delete = null;
		if(behaviourTree.GetGUINodes().Count > 0)
		{
			for(int i = 0; i < behaviourTree.GetGUINodes().Count; ++i)
			{
				GUINode node = behaviourTree.GetGUINodes()[i];
				
				wasDragging = node.IsDragging;
				
				node.OnGUI();
				
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
				else if(Event.current.type == EventType.MouseDown && node.IsMouseHover(node.getMainRect()) && Event.current.button == 0)
				{
					//Debug.Log ("select");
					SelectNode(node);
				}
				
			}
		}
		
		GUILayout.EndScrollView();

		if(delete != null)
		{
			Debug.Log ("delete");
			DeselectNode();
			DeleteNode(delete);
		}
	}

	void DrawMenuArea()
	{
		GUILayout.BeginHorizontal("box", GUILayout.Width(currentToolWidth), GUILayout.ExpandWidth(true));
			int buttonWidth = 50;
			if(GUILayout.Button("Load", GUILayout.MaxWidth(buttonWidth)))
			{
				if(behaviourTree.GetBehaviour() == null)
				{
					string path = EditorUtility.OpenFilePanel("Open Behaviour File...", "Assets\\Resources\\Behaviour Trees", behaviourTree.GetFactory().GetFileExtension());
					if(path != "")
					{
						behaviourTree.GetFactory().ReadFromPath(path);
						behaviourTree.SetCurrentFilePath(path);
					}
				}
				else if(EditorUtility.DisplayDialog("Load Behaviour Tree", "Do you really want to load a Behaviour Tree and delete all current Nodes?", "Yes", "No"))
				{
					DeleteAll();
					string path = EditorUtility.OpenFilePanel("Open Behaviour File...", "Assets\\Resources\\Behaviour Trees", behaviourTree.GetFactory().GetFileExtension());
					if(path != "")
					{
						behaviourTree.GetFactory().ReadFromPath(path);
						behaviourTree.SetCurrentFilePath(path);
					}
				}
			}
			if(GUILayout.Button("Save", GUILayout.MaxWidth(buttonWidth)))
			{
				string path = EditorUtility.SaveFilePanel("Save Behaviour as..", "Assets\\Resources\\Behaviour Trees", "BehaviourTree", behaviourTree.GetFactory().GetFileExtension());
				if(path != "")
				{
					behaviourTree.GetFactory().SaveToPath(path);
					behaviourTree.SetCurrentFilePath(path);
					saved = true;
				}
			}
		GUILayout.EndHorizontal();
		
		//dont know why this
		GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

		menuScrollPos = GUILayout.BeginScrollView(menuScrollPos, GUILayout.Width(currentToolWidth+7), GUILayout.ExpandHeight(true));
		GUILayout.BeginVertical("box", GUILayout.Width(currentToolWidth), GUILayout.ExpandHeight(true));
			EditorGUI.BeginDisabledGroup(behaviourTree.GetGUINodes().Count > 0);
				GUI.color = Color.cyan;
				if(GUILayout.Button("New Behaviour"))
				{
					behaviourTree.GetFactory().Create ("Behaviour", scrollPos + mainAreaRect.center + new Vector2 ( 0, -mainAreaRect.height/2)- new Vector2(currentToolWidth, -50), null); 
						SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
					}
					GUI.color = Color.white;
				EditorGUI.EndDisabledGroup();
				EditorGUI.BeginDisabledGroup(selected == null || !selected.CanHaveMoreChildren());
				GUI.color = Color.yellow;
				showDecorators = EditorGUILayout.Foldout(showDecorators, "Decorators");
				if(showDecorators)
				{		
					if(GUILayout.Button("New Selector"))
					{
							behaviourTree.GetFactory().Create("Selector", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New Sequence"))
						{
							behaviourTree.GetFactory().Create("Sequence", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New True"))
						{
							behaviourTree.GetFactory().Create("True", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New False"))
						{
							behaviourTree.GetFactory().Create("False", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New Inverter"))
						{
							behaviourTree.GetFactory().Create("Inverter", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New Parallel"))
						{
							behaviourTree.GetFactory().Create("Parallel", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New ParallelOneForAll"))
						{
							behaviourTree.GetFactory().Create("ParallelOneForAll", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New UntilFail"))
						{
							behaviourTree.GetFactory().Create("UntilFail", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}

					}
					GUI.color = new Color(0.5f, 0.6f, 1.0f);
					showTasks = EditorGUILayout.Foldout(showTasks, "Tasks");
					if(showTasks)
					{
						if(GUILayout.Button("New AttackTask"))
						{
							behaviourTree.GetFactory().Create("AttackTask", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New SeekOpponentTask"))
						{
							behaviourTree.GetFactory().Create("SeekOpponentTask", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New EvadeOpponentTask"))
						{
							behaviourTree.GetFactory().Create("EvadeOpponentTask", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New FlankOpponentTask"))
						{
							behaviourTree.GetFactory().Create("FlankOpponentTask", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New MaintainDistanceTask"))
						{
							behaviourTree.GetFactory().Create("MaintainDistanceTask", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New SeekNearestHealthTask"))
						{
							behaviourTree.GetFactory().Create("SeekNearestHealthTask", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
					}
					GUI.color = Color.green;
					showConditions = EditorGUILayout.Foldout(showConditions, "Conditions");
					if(showConditions)
					{
						if(GUILayout.Button("New AreYouHurt"))
						{
							behaviourTree.GetFactory().Create("AreYouHurt", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}
						if(GUILayout.Button("New IsOpponentMoreHurt"))
						{
							behaviourTree.GetFactory().Create("IsOpponentMoreHurt", selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
							SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
						}

					}
					GUI.color = Color.white;
				EditorGUI.EndDisabledGroup();

				GUILayout.Space(50);
				GUI.color = Color.red;
				if(GUILayout.Button("Delete All"))
				{
					if(EditorUtility.DisplayDialog("Delete All", "Do you really want to delete all Nodes?", "Yes", "No"))
					{
						DeleteAll();
					}
				}
				
				GUI.color = Color.white;
			GUILayout.EndVertical();
		GUILayout.EndScrollView();
	}

	private void MoveAround()
	{
		if(Event.current.type == EventType.MouseDrag && Event.current.button == 2)
		{
				scrollPos += Event.current.delta;
				Event.current.Use();
		}
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
		
		saved = false;
	}

	void DeselectNode()
	{
		if(selected != null)
		{
			selected.Select = false;
			selected = null;
		}
	}

	void DeleteNode(GUINode node)
	{	
		saved = false;
		List<GUINode> deleteList = node.GetAllChildren();	
		node.GetModel().Delete();
		foreach(GUINode g in deleteList)
		{
			behaviourTree.GetGUINodes().Remove(g);
		}
		behaviourTree.GetGUINodes().Remove(node);

	}

	void DeleteAll()
	{
		DeselectNode();
		DeleteNode(behaviourTree.GetBehaviour().GetView());
		behaviourTree.GetGUINodes().Clear();
		behaviourTree.ClearNodes();
	}

	public void OnDestroy()
	{
		if((!saved) && EditorUtility.DisplayDialog("Save before closing", "Do you want to save before closing?", "Save", "Close without Saving"))
		{
			string path = EditorUtility.SaveFilePanel("Save Behaviour as..", "Assets\\Resources\\Behaviour Trees", "BehaviourTree", behaviourTree.GetFactory().GetFileExtension());
			if(path != "")
			{
				behaviourTree.GetFactory().SaveToPath(path);
				behaviourTree.SetCurrentFilePath(path);
			}
		}
		DeselectNode();
		behaviourTree.GetFactory().SaveToPath("Assets\\Resources\\Behaviour Trees\\Backups\\backupSave.txt");
		DeleteAll();
		Repaint ();
	}

}
