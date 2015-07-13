using UnityEngine;
using System.Collections;

public interface ChildNode
{
	void activate();
}

public interface ParentNode
{
	void AddChild(ChildNode c);
	bool childDone(ChildNode c);
}

public interface TaskNode
{
	void performTask();
}
