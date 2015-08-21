using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//A Baseclass for all Draggable GUI elements
public class DraggableGUIElement 
{
	protected Vector2 position;
	private Vector2 dragOffset;

	private bool isDragging = false;

	protected Color color = Color.white;



	public DraggableGUIElement(Vector2 position)
	{
		this.position = position;
	}


	public void Drag(Rect dragRect)
	{
		//If the mouse is released, stop dragging
		if(isDragging && Event.current.type == EventType.MouseUp && Event.current.button == 0)
		{
			isDragging = false;
			//Debug.Log ("stopDrag");
		}
		//if the mouse is pressed within the target rectangle(bounds of the GUIElement) start dragging
		else if(Event.current.type == EventType.MouseDrag && Event.current.button == 0 && dragRect.Contains(Event.current.mousePosition))
		{
			//Debug.Log("startdrag");
			isDragging = true;
			dragOffset = Event.current.mousePosition - position;
			Event.current.Use();
		}
		if(isDragging)
		{
			position = Event.current.mousePosition - dragOffset;
			DragUpdate();
		}
	}

	public virtual void DragUpdate()
	{

	}

	public bool IsMouseHover(Rect dragRect)
	{
		return dragRect.Contains(Event.current.mousePosition);
		
	}

	//Getter and Setter        
	public Vector2 Position
	{
		get
		{
			return position;
		}

		set
		{
			position = value;
		}
	}

	public bool IsDragging
	{
		get
		{
			return isDragging;
		}
	}

	public Color ElementColor
	{
		get
		{
			return color;
		}
		set
		{
			color = value;
		}
	}


}
