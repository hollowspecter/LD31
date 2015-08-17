using UnityEngine;
using System.Collections;

public interface Node
{
	GUINode GetView();
	void Delete();
}

public interface ChildNode : Node
{
	void Activate();	
	void Deactivate();

	//void SetParent(ParentNode parent);
}

public interface ParentNode : Node
{
	void AddChild(ChildNode child);
	void RemoveChild(ChildNode child);
	void ChildDone(ChildNode child, bool childResult);
}

public interface TaskNode : ChildNode
{
	void PerformTask();
}


