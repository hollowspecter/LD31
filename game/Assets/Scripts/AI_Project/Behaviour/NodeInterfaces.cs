using UnityEngine;
using System.Collections;

public interface ChildNode
{
	void Activate();	
	void Deactivate();

	//void SetParent(ParentNode parent);
}

public interface ParentNode
{
	void AddChild(ChildNode child);
	void ChildDone(ChildNode child, bool childResult);
}

public interface TaskNode : ChildNode
{
	void PerformTask();
}


