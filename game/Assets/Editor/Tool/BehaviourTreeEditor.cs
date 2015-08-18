using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(BehaviourTree))]
public class BehaviourTreeEditor : Editor 
{
	BehaviourTree behaviourTree;


	public void OnEnable()
	{
		behaviourTree = (BehaviourTree)target;
	}

	// Override the default Editor
	public override void OnInspectorGUI()
	{
		GUILayout.Label(behaviourTree.GetCurrentFileName());
		if (GUILayout.Button("Edit Behaviour Tree", GUILayout.Width(255)))
		{   
			BehaviourWindow window = (BehaviourWindow) EditorWindow.GetWindow(typeof(BehaviourWindow));
			window.Init();
		}
	}

}
