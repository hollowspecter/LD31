using UnityEngine;
using System.Collections;

public interface ChildNode
{
	void Activate();
}

public interface ParentNode
{
	void AddChild(ChildNode c);
	bool ChildDone(ChildNode c);
}

public interface TaskNode
{
	void PerformTask();
}


