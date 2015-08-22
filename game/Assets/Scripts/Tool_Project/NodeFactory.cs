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

			/***DECORATORS****/

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

			case "True":
			{
				CreateTrue(nodeID++, guiPosition, parentNode);
				break;
			}

			case "False":
			{
				CreateFalse(nodeID++, guiPosition, parentNode);
				break;
			}

			case "Inverter":
			{
				CreateInverter(nodeID++, guiPosition, parentNode);
				break;
			}

			case "Parallel":
			{
				CreateParallel(nodeID++, guiPosition, parentNode);
				break;
			}

			case "ParallelOneForAll":
			{
				CreateParallelOneForAll(nodeID++, guiPosition, parentNode);
				break;
			}

			case "UntilFail":
			{
				CreateUntilFail(nodeID++, guiPosition, parentNode);
				break;
			}

			/****TASKS*****/

			case "AttackTask":
			{
				CreateAttackTask(nodeID++, guiPosition, parentNode);
				break;
			}

			case "SeekOpponentTask":
			{
				CreateSeekOpponentTask(nodeID++, guiPosition, parentNode);
				break;
			}

			case "EvadeOpponentTask":
			{
				CreateEvadeOpponentTask(nodeID++, guiPosition, parentNode);
				break;
			}

			case "FlankOpponentTask":
			{
				CreateFlankOpponentTask(nodeID++, guiPosition, parentNode);
				break;
			}

			case "MaintainDistanceTask":
			{
				CreateMaintainDistanceTask(nodeID++, guiPosition, parentNode);
				break;
			}

			case "SeekNearestHealthTask":
			{
				CreateSeekNearestHealthTask(nodeID++, guiPosition, parentNode);
				break;
			}

			/***CONDITIONS******/
			case "AreYouHurt":
			{
				CreateAreYouHurt(nodeID++, guiPosition, parentNode);
				break;
			}
				
			case "IsOpponentMoreHurt":
			{
				CreateIsOpponentMoreHurt(nodeID++, guiPosition, parentNode);
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
				Debug.Log ("Create Sequence: ID:" + NodeID + " Parent:" + parentID);
				CreateSequence(NodeID, guiPosition, parentNode);
				break;
			}

			case 3:
			{
				Debug.Log ("Create True: ID:" + NodeID + " Parent:" + parentID);
				CreateTrue(NodeID, guiPosition, parentNode);
				break;
			}
				
			case 4:
			{
				Debug.Log ("Create False: ID:" + NodeID + " Parent:" + parentID);
				CreateFalse(NodeID, guiPosition, parentNode);
				break;
			}
				
			case 5:
			{
				Debug.Log ("Create Inverter: ID:" + NodeID + " Parent:" + parentID);
				CreateInverter(NodeID, guiPosition, parentNode);
				break;
			}
				
			case 6:
			{
				Debug.Log ("Create Parallel: ID:" + NodeID + " Parent:" + parentID);
				CreateParallel(NodeID, guiPosition, parentNode);
				break;
			}
				
			case 7:
			{
				Debug.Log ("Create ParallelOneForAll: ID:" + NodeID + " Parent:" + parentID);
				CreateParallelOneForAll(NodeID, guiPosition, parentNode);
				break;
			}
				
			case 8:
			{
				Debug.Log ("Create UntilFail: ID:" + NodeID + " Parent:" + parentID);
				CreateUntilFail(NodeID, guiPosition, parentNode);
				break;
			}

			case 9:
			{
				Debug.Log ("Create AttackTask: ID:" + NodeID + " Parent:" + parentID);
				CreateAttackTask(NodeID, guiPosition, parentNode);
				break;
			}

			case 10:
			{
				Debug.Log ("Create SeekOpponentTask: ID:" + NodeID + " Parent:" + parentID);
				CreateSeekOpponentTask(NodeID, guiPosition, parentNode);
				break;
			}
			case 11:
			{
				Debug.Log ("Create EvadeOpponentTask: ID:" + NodeID + " Parent:" + parentID);
				CreateEvadeOpponentTask(NodeID, guiPosition, parentNode);
				break;
			}
			case 12:
			{
				Debug.Log ("Create FlankOpponentTask: ID:" + NodeID + " Parent:" + parentID);
				CreateFlankOpponentTask(NodeID, guiPosition, parentNode);
				break;
			}
			case 13:
			{
				Debug.Log ("Create MaintainDistanceTask: ID:" + NodeID + " Parent:" + parentID);
				CreateMaintainDistanceTask(NodeID, guiPosition, parentNode);
				break;
			}
			case 14:
			{
				Debug.Log ("Create SeekNearestHealthTask: ID:" + NodeID + " Parent:" + parentID);
				CreateSeekNearestHealthTask(NodeID, guiPosition, parentNode);
				break;
			}

			/***CONDITIONS******/
			case 15:
			{
				CreateAreYouHurt(NodeID, guiPosition, parentNode);
				break;
			}
				
			case 16:
			{
				CreateIsOpponentMoreHurt(NodeID, guiPosition, parentNode);
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
			//Debug.Log (i + "\n" + subContent);
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
					Debug.Log("matching id = " + parentID);
					return (ParentNode)node;
				}
				else
				{
					string.Concat(other, node.GetView().GetNodeID().ToString());
					string.Concat(other, " ");
					Debug.Log ("otherID: " + other);
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


	/***********CREATION METHODS************/
	//All of these Methods are Basically the Same and just differ in the Type of Nodes they create.
	//For additional Types just Copy-Paste a Method of the corresponding Group (Decorator, Task or Condition)
	//and replace the Types.
	

	void CreateBehaviour(int NodeID, Vector2 guiPosition)
	{
		GUIBehaviour gui = new GUIBehaviour(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		Behaviour node = new Behaviour(gui, behaviourTree);
		behaviourTree.SetBehaviour(node);
	}

	/***********DECORATORS***************/

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

	void CreateTrue(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUITrue gui = new GUITrue(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		True node = new True(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateFalse(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIFalse gui = new GUIFalse(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		False node = new False(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateInverter(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIInverter gui = new GUIInverter(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		Inverter node = new Inverter(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateParallel(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIParallel gui = new GUIParallel(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		Parallel node = new Parallel(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateParallelOneForAll(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIParallelOneForAll gui = new GUIParallelOneForAll(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		ParallelOneForAll node = new ParallelOneForAll(parent, gui);
		behaviourTree.AddNode(node);
	}

	void CreateUntilFail(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIUntilFail gui = new GUIUntilFail(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		UntilFail node = new UntilFail(parent, gui);
		behaviourTree.AddNode(node);
	}

	/**********TASKS*********************/

	void CreateAttackTask (int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIAttackTask gui = new GUIAttackTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		AttackTask node = new AttackTask(parent, behaviourTree.GetBehaviour(), gui);
		behaviourTree.AddNode(node);
	}

	void CreateSeekOpponentTask(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUISeekOpponentTask gui = new GUISeekOpponentTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		SeekOpponentTask node = new SeekOpponentTask(parent, behaviourTree.GetBehaviour(), gui);
		behaviourTree.AddNode(node);
	}

	void CreateEvadeOpponentTask(int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIEvadeOpponentTask gui = new GUIEvadeOpponentTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		EvadeOpponentTask node = new EvadeOpponentTask(parent, behaviourTree.GetBehaviour(), gui);
		behaviourTree.AddNode(node);
	}

	void CreateFlankOpponentTask (int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIFlankOpponentTask gui = new GUIFlankOpponentTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		FlankOpponentTask node = new FlankOpponentTask(parent, behaviourTree.GetBehaviour(), 9.0f, gui);
		behaviourTree.AddNode(node);
	}

	void CreateMaintainDistanceTask (int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIMaintainDistanceTask gui = new GUIMaintainDistanceTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		MaintainDistanceTask node = new MaintainDistanceTask(parent, behaviourTree.GetBehaviour(), gui);
		behaviourTree.AddNode(node);
	}

	void CreateSeekNearestHealthTask (int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUISeekNearestHealthTask gui = new GUISeekNearestHealthTask(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		SeekNearestHealthTask node = new SeekNearestHealthTask(parent, behaviourTree.GetBehaviour(), gui);
		behaviourTree.AddNode(node);
	}


	/**********CONDITIONS****************/
	void CreateAreYouHurt (int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIAreYouHurt gui = new GUIAreYouHurt(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		AreYouHurt node = new AreYouHurt(parent, true, gui);
		behaviourTree.AddNode(node);
	}
	
	void CreateIsOpponentMoreHurt (int NodeID, Vector2 guiPosition, ParentNode parent)
	{
		GUIIsOpponentMoreHurt gui = new GUIIsOpponentMoreHurt(NodeID, guiPosition);
		behaviourTree.AddGUINode(gui);
		IsOpponentMoreHurt node = new IsOpponentMoreHurt(parent, true, gui);
		behaviourTree.AddNode(node);
	}

}
