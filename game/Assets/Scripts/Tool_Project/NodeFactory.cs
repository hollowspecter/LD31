using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class NodeFactory 
{
	BehaviourTree tree;

	int nodeID;

	//TypeID -> Type
	//0 -> Behaviour
	//1 -> Selector
	//2 -> Sequence
	//

	// Use this for initialization
	public NodeFactory(BehaviourTree t) 
	{
		tree = t;
		nodeID = 0;
	}

	public void ResetID()
	{
		nodeID = 0;
	}
	
	public void Create(string type, Vector2 guiPosition, ParentNode parentNode)
	{
		switch(type)
		{
			case "Behaviour":
			{
				CreateBehaviour(nodeID++, guiPosition);
				break;
			}

			case "Selector":
			{
				CreateSelector(nodeID++, guiPosition, parentNode);
				break;
			}
			case "Sequence":
			{
				CreateSequence(nodeID++, guiPosition, parentNode);
				break;
			}
		}
	}

	void CreateFromFile(int TypeID, int NodeID, int parentID, int guiPosX, int guiPosY)
	{
		if(nodeID <= NodeID)
		{
			nodeID = (NodeID + 1);
		}

		Vector2 guiPosition = new Vector2(guiPosX, guiPosY);

		ParentNode parentNode = FindParent(parentID);

		switch(TypeID)
		{
			case 0:
			{
				Debug.Log ("Create Behaviour: ID:" + NodeID);
				CreateBehaviour(NodeID,guiPosition);
				break;
			}
			case 1:
			{
				Debug.Log ("Create Selector: ID:" + NodeID + " Parent:" + parentID);
				CreateSelector(NodeID,guiPosition, parentNode);
				break;
			}

			case 2:
			{
				CreateSequence(NodeID, guiPosition, parentNode);
				break;
			}

		}
	}

	public void SaveToFile(string fileName)
	{

	}

	public void ReadFile(string fileName)
	{
		TextAsset file = EditorGUIUtility.Load (fileName) as TextAsset;
		if(file != null)
		{
			string content = file.text;

			do
			{
				int j = content.IndexOf("/");
				string subContent =content.Substring(0, j+1);

				//parse substring into ints
				int type, node, parent, x, y;
				int i = subContent.IndexOf(">");
			    type = int.Parse(subContent.Substring(5, i-5));
				subContent = subContent.Substring(i+2);
				i = subContent.IndexOf(">");
				node = int.Parse(subContent.Substring(5, i-5));
				subContent = subContent.Substring(i+2);
				i = subContent.IndexOf(">");
				parent = int.Parse(subContent.Substring(7, i-7));
				subContent = subContent.Substring(i+2);
				i = subContent.IndexOf(">");
				x = int.Parse(subContent.Substring(2, i-2));
				subContent = subContent.Substring(i+2);
				i = subContent.IndexOf(">");
				y = int.Parse(subContent.Substring(2, i-2));
				Debug.Log (node + " " + type + " " + parent);

				CreateFromFile(type, node, parent, x, y);

				if(j+2 <= content.Length)
				{
					content = content.Substring(j+2);
				}
				else
				{
					content = null;
				}
			}while(content != null);
			tree.SetCurrentFileName(fileName);
		}
		else
		{
			Debug.Log ("no such file");
		}
	}

	ParentNode FindParent(int parentID)
	{
		string other = "";
		if(tree.GetBehaviour() != null && tree.GetBehaviour().GetView().GetNodeID() == parentID)
		{
			return tree.GetBehaviour();
		}
		else
		{
			for(int i = 0; i < tree.GetCount(); i++)
			{
				Node node = tree.GetNodeAt(i);
				if(node.GetView().GetNodeID() == parentID)
				{
					return (ParentNode)node;
				}
				else
				{
					string.Concat(other, node.GetView().GetNodeID().ToString());
					string.Concat(other, " ");
				}
			}
		}
		Debug.Log ("no node has this id:" + parentID + "\notherIDs:" + other);
		return null;
	}

	void CreateBehaviour(int NodeID, Vector2 guiPosition)
	{
		GUIBehaviour gui = new GUIBehaviour(NodeID, guiPosition);
		tree.AddGUINode(gui);
		Behaviour node = new Behaviour(gui, tree);
		tree.SetBehaviour(node);
	}

	void CreateSelector(int NodeID, Vector2 guiPosition, ParentNode parent)
	{

		GUISelector gui = new GUISelector(NodeID, guiPosition);
		tree.AddGUINode(gui);
		Selector node = new Selector(parent, gui);
		tree.AddNode(node);
	}

	void CreateSequence(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUISequence gui = new GUISequence(NodeID, guiPosition);
		tree.AddGUINode(gui);
		Sequence node = new Sequence(parent, gui);
		tree.AddNode(node);
	}
}
