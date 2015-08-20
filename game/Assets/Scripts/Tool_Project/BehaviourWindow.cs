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

	string lastTreePath;

	Rect mainAreaRect;

	Rect deleteRect;

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
		if(behaviourTree == null)
		{
			behaviourTree = GameObject.FindGameObjectWithTag("Player1").GetComponent<BehaviourTree>();
		}
		behaviourTree.Init();
		BehaviourWindow window = (BehaviourWindow)EditorWindow.GetWindow (typeof (BehaviourWindow));
		window.titleContent.text = "Behaviour";
		window.titleContent.tooltip = "Here you can edit your Behaviour Tree";
		currentToolWidth = this.position.width/3;
		maxToolWidth = this.position.width - minToolWidth;
		cursorChangeRect = new Rect(currentToolWidth-5, 20, 10,this.position.height);

		mainAreaRect = new Rect(currentToolWidth, 20, position.width-currentToolWidth, position.height);
		deleteRect = new Rect(7, mainAreaRect.height - 200, 100, 170);
		selected = null;

		scrollPos = Vector2.zero;

		if(!(behaviourTree.GetCurrentFilePath() == ""))
		{
			behaviourTree.GetFactory().ReadFromPath(behaviourTree.GetCurrentFilePath());
		}
	}

	void OnGUI()
	{	
		GUINode delete;
		
		bool wasDragging;
		Color color;

		GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));
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
				}
			}
		GUILayout.EndHorizontal();

		//left Toolbar
		GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

			GUILayout.BeginVertical("box", GUILayout.Width(currentToolWidth), GUILayout.ExpandHeight(true));
				EditorGUI.BeginDisabledGroup(behaviourTree.GetGUINodes().Count > 0);
				if(GUILayout.Button("New Behaviour"))
		 		{
					behaviourTree.GetFactory().Create ("Behaviour", scrollPos + mainAreaRect.center + new Vector2 ( 0, -mainAreaRect.height/2)- new Vector2(currentToolWidth, 0), null); 
					SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
				}
				EditorGUI.EndDisabledGroup();
				EditorGUI.BeginDisabledGroup(selected == null || !selected.CanHaveMoreChildren());
					if(GUILayout.Button("New Selector"))
					{
						behaviourTree.GetFactory().Create("Selector", scrollPos +  selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
						SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
					}
					if(GUILayout.Button("New Sequence"))
					{
						behaviourTree.GetFactory().Create("Sequence", scrollPos +  selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
						SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
					}
					if(GUILayout.Button("New SeekOpponentTask"))
					{
						behaviourTree.GetFactory().Create("SeekOpponentTask", scrollPos +  selected.Position + new Vector2(0, 200), (ParentNode)selected.GetModel());
						SelectNode(behaviourTree.GetGUINodes()[behaviourTree.GetGUINodes().Count-1]);
					}
				EditorGUI.EndDisabledGroup();
				
				GUILayout.Space(280);
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
			scrollPos = GUI.BeginScrollView(mainAreaRect, scrollPos, new Rect(0,0, 10000, 2000),true, true);
		
				GUI.color = Color.red;
				GUILayout.BeginArea(deleteRect, GUI.skin.GetStyle("Box"));
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
						
						if(i+1 < behaviourTree.GetGUINodes().Count)
						{/*
							Handles.BeginGUI();
							Handles.color = Color.black;
							Vector3 start = new Vector3(node.GetBotPosition().x, node.GetBotPosition().y);
							Vector3 end = new Vector3(behaviourTree.GetGUINodes()[i+1].GetTopPosition().x, behaviourTree.GetGUINodes()[i+1].GetTopPosition().y);
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

	public void AddNode(GUINode node)
	{
		behaviourTree.GetGUINodes().Add(node);
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
		if(selected != null)
		{
			selected.Select = false;
			selected = null;
		}
	}

	void DeleteNode(GUINode node)
	{	
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
		DeselectNode();
		behaviourTree.GetFactory().SaveToPath("Assets\\Resources\\Behaviour Trees\\Backups\\backupSave.txt");
		Repaint ();
	}

}
