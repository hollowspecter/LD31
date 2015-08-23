using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class NodeFactory 
{
	BehaviourTree behaviourTree;

	int nodeID;

	List<Type> nodeTypes;

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

	public void Init()
	{
		nodeTypes = GetClasses(typeof(Node));

	}

	
	public void CreateNode(string typeName, Vector2 guiPosition, ParentNode parentNode)
	{
		Type correctType = null;

		foreach(Type type in nodeTypes)
		{
			if(type.Name == typeName)
			{
				correctType = type;
				break;
			}
		}
		if(correctType != null)
		{
			ConstructorInfo ctor =  correctType.GetConstructor(Type.EmptyTypes);
			object instance = ctor.Invoke(Type.EmptyTypes);
			correctType.GetMethod("Create").Invoke(instance, new object[] {nodeID++, guiPosition, parentNode, behaviourTree});
			
		}
	}

	public void CreateNodeFromFile(int TypeID, int NodeID, int ParentID, Vector2 guiPosition)
	{
		if(NodeID >= nodeID)
		{
			nodeID = NodeID + 1;
		}
		string typeName = FindTypeName(TypeID);
		ParentNode parentNode = null;
		if(ParentID >= 0)
			parentNode = FindParent(ParentID);

		Type correctType = null;
		
		foreach(Type type in nodeTypes)
		{
			if(type.Name == typeName)
			{
				correctType = type;
				break;
			}
		}
		if(correctType != null)
		{
			ConstructorInfo ctor =  correctType.GetConstructor(Type.EmptyTypes);
			object instance = ctor.Invoke(Type.EmptyTypes);
			correctType.GetMethod("Create").Invoke(instance, new object[] {NodeID, guiPosition, parentNode, behaviourTree});
			
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

	public void SaveToPath(string path)
	{
		StreamWriter writer = new StreamWriter(path);
		foreach(GUINode gui in behaviourTree.GetGUINodes())
		{
			writer.Write("Type<" + gui.GetModel().GetTypeID() + ">\n");
			writer.Write("Node<" + gui.GetNodeID() + ">\n");
			int parentID = -1;
			if(gui.GetModel().GetTypeID() != 0)
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
			string subContent = content.Substring(0, j+1);
			
			//parse substring into ints
			int type, node, parent;
			Vector2 position = new Vector2();
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
			position.x = int.Parse(subContent.Substring(2, i-2));
			subContent = subContent.Substring(i+2);
			i = subContent.IndexOf(">");
			position.y = int.Parse(subContent.Substring(2, i-2));
			
			CreateNodeFromFile(type, node, parent, position); 
			
			if(j+2 <= content.Length)
			{
				content = content.Substring(j+2);
				Debug.Log (content);
			}
			else
			{
				content = "";
				Debug.Log ("File is empty");
			}
		}while(content != "");
		Debug.Log ("read file to end");
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

	string FindTypeName(int typeID)
	{
		foreach(Type type in nodeTypes)
		{
			FieldInfo i = type.GetField("TypeID");
			if(i != null)
			{
				object o = i.GetValue(null);
				if(typeID == (int)o)
				{
					return type.Name;
				}
				Debug.Log ("IDs do not match " + typeID + "|" + (int)o);

			}
			else
			{
				Debug.Log ("no property like that");
			}
		}
		return "";
	}

	public string GetFileExtension()
	{
		return fileExtension;
	}

	public List<Type> GetClasses(Type baseType)
	{
		Type[] array = Assembly.GetCallingAssembly().GetTypes();
		List<Type> res = new List<Type>();
		string t = "List of Types: \n";
		foreach(Type type in array)
		{
			if(baseType.IsAssignableFrom(type) && !type.IsInterface )
			{
				res.Add(type);
				t += type.Name + "\n";
			}
		}
		
		Debug.Log (t);
		return res;
	}
	
	public List<string> GetNodeSubTypeName(Type subType)
	{
		List<string> res = new List<string>();
		string t = "List for Subtype:" + subType.Name + "\n";
		
		foreach(Type type in behaviourTree.GetFactory().GetNodeTypes())
		{
			if(subType.IsAssignableFrom(type))
			{
				res.Add(type.Name);
				t += type.Name + "\n";
			}
		}
		
		
		Debug.Log (t);
		return res;
	}
	
	public List<Type> GetNodeTypes()
	{
		return nodeTypes;
	}

}
