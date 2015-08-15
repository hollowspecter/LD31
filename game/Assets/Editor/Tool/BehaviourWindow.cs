using UnityEngine;
using UnityEditor;
using System.Collections;

public class BehaviourWindow : EditorWindow
{	
	//[MenuItem("Window/Test Editor")]

	BehaviourTree behaviour;

	float currentToolWidth;
	float minToolWidth = 285;
	float maxToolWidth;
	Rect cursorChangeRect;

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
	}

	void OnGUI()
	{	
		GUILayout.Toolbar(-1, new string[]{"Tool0", "Tool1", "Tool2", "Tool3", "Tool4", "Tool5"});

		//left Toolbar
		GUILayout.BeginVertical("box", GUILayout.Width(currentToolWidth), GUILayout.ExpandHeight(true));
		Debug.Log (currentToolWidth);
		behaviour.color = EditorGUILayout.ColorField(behaviour.color, GUILayout.Width(200));

		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		behaviour.myString = EditorGUILayout.TextField ("Text Field", behaviour.myString);
		
		behaviour.groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", behaviour.groupEnabled);
		behaviour.myBool = EditorGUILayout.Toggle ("Toggle", behaviour.myBool);
		behaviour.myFloat = EditorGUILayout.Slider ("Slider", behaviour.myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();


		GUILayout.Button("Button1");
		GUILayout.Button("Button2");
		GUILayout.Button("Button3");
		GUILayout.EndVertical();

		ResizeBar();
		Repaint();

	}

	void ResizeBar()
	{		
		maxToolWidth = this.position.width - minToolWidth;
		GUI.DrawTexture(cursorChangeRect,EditorGUIUtility.whiteTexture);
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

}
