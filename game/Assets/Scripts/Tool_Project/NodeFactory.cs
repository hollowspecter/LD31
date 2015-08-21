using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class NodeFactory 
{
	BehaviourTree behaviourTree;

	int nodeID;

	//TypeID -> Type
	//0 -> Behaviour
	//1 -> Selector
	//2 -> Sequence
	//3 -> True
	//4 -> False
	//5 -> Inverter
	//6 -> Parallel
	//7 -> ParallelOneForAll
	//8 -> UntilFail
	//9 -> AttackTask
	//10 ->SeekOpponentTask
	//11 ->EvadeOpponentTask
	//12 ->FlankOpponentTask
	//13 ->MaintainDistanceTask
	//14 ->SeekNearestHealthTask

	string fileExtension = "txt";

	// Use this for initialization
	public NodeFactory(BehaviourTree t) 
	{
		behaviourTree = t;
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

			case "SeekOpponentTask":
			{
				CreateSeekOpponentTask(nodeID++, guiPosition, parentNode);
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

			case 10:
			{
				CreateSeekOpponentTask(nodeID++, guiPosition, parentNode);
				break;
			}

		}
	}

	//cuts . from fileName and removes anything behind that
	string CutFileExtension(string fileName)
	{
		string tmp = fileName;
		if(fileName.Contains ("."))
		{
			int i = fileName.IndexOf(".");
			tmp = fileName.Substring (0, i);
		}

		return tmp;
	}

	public void CheckForExistingFile(string fileName)
	{

	}

	public void SaveToPath(string path)
	{
		StreamWriter writer = new StreamWriter(path);
		foreach(GUINode gui in behaviourTree.GetGUINodes())
		{
			writer.Write("Type<" + gui.GetTypeID() + ">\n");
			writer.Write("Node<" + gui.GetNodeID() + ">\n");
			int parentID = -1;
			if(gui.GetTypeID() != 0)
			{
				ChildNode c = (ChildNode)(gui.GetModel());
				parentID = c.GetParent().GetView().GetNodeID();
			}
			writer.Write("Parent<" + parentID + ">\n");
			writer.Write("X<" + (int)gui.Position.x + ">\n");
			writer.Write("Y<" + (int)gui.Position.y + ">\n");
			writer.Write("/\n");
		}
		writer.Flush();
		writer.Close();

	}

	public void ReadFromPath(string path)
	{
		StreamReader reader = new StreamReader(path);
		string content = reader.ReadToEnd();

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
			Debug.Log (i + "\n" + subContent);
			x = int.Parse(subContent.Substring(2, i-2));
			subContent = subContent.Substring(i+2);
			i = subContent.IndexOf(">");
			y = int.Parse(subContent.Substring(2, i-2));
			
			CreateFromFile(type, node, parent, x, y);
			
			if(j+2 <= content.Length)
			{
				content = content.Substring(j+2);
			}
			else
			{
				content = "";
			}
		}while(content != "");
		reader.Close();
	}

	ParentNode FindParent(int parentID)
	{
		string other = "";
		if(behaviourTree.GetBehaviour() != null && behaviourTree.GetBehaviour().GetView().GetNodeID() == parentID)
		{
			return behaviourTree.GetBehaviour();
		}
		else
		{
			for(int i = 0; i < behaviourTree.GetCount(); i++)
			{
				Node node = behaviourTree.GetNodeAt(i);
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

	public string GetFileExtension()
	{
		return fileExtension;
	}

	void CreateBehaviour(int NodeID, Vector2 guiPosition)
	{
		GUIBehaviour gui = new GUIBehaviour(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		Behaviour node = new Behaviour(gui, behaviourTree);
		behaviourTree.SetBehaviour(node);
	}

	void CreateSelector(int NodeID, Vector2 guiPosition, ParentNode parent)
	{

		GUISelector gui = new GUISelector(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		Selector node = new Selector(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateSequence(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUISequence gui = new GUISequence(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		Sequence node = new Sequence(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateSeekOpponentTask(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		Debug.Log ("createSeekTask");
		GUISeekOpponentTask gui = new GUISeekOpponentTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		SeekOpponentTask node = new SeekOpponentTask(parent, behaviourTree.GetBehaviour(), gui);
		if(node.GetParent() == null)
		{
			Debug.Log ("node null");
		}
		behaviourTree.AddNode(node);
	}


}
