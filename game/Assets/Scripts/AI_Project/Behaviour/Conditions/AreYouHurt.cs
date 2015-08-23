using UnityEngine;
using System.Collections;

public class AreYouHurt: ConditionNode
{
	public static int TypeID = 15;

	ParentNode parent;

	GUIAreYouHurt view;

	PlayerStance ownStance;

	float hurtValue = 33.0f;
	bool randomize = false;

	public AreYouHurt(ParentNode p, bool randomize, GUIAreYouHurt view)
	{
		parent = p;
		parent.AddChild(this);

		this.view = view;
		this.view.SetModel(this);

		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}
		ownStance = self.GetComponent<PlayerStance>();
		
		this.randomize = randomize;
	}

	public AreYouHurt()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create AreYouHurt");
		GUIAreYouHurt gui = new GUIAreYouHurt(nodeID, guiPosition);
		tree.AddGUINode(gui);
		
		this.parent = parent;
		parent.AddChild(this);
		
		this.view = gui;
		this.view.SetModel(this);
		
		GameObject self = GameObject.FindGameObjectWithTag("Player1");
		if(self == null)
		{
			self = GameObject.Find("AI_Deer");
		}
		ownStance = self.GetComponent<PlayerStance>();

		this.randomize = true;
		
		tree.AddNode(this);
	}

	public void Activate()
	{
		Check ();
	}

	public void Deactivate()
	{

	}
	
	public void Check()
	{
		Debug.Log ("are you hurt?");
		float tmpHurt = hurtValue;
		if(randomize)
		{
			tmpHurt += Random.Range(-hurtValue/3, hurtValue/3);
		}
		
		bool areYouHurt = (ownStance.currentMultiplier > tmpHurt);
		if(areYouHurt)
			Debug.Log("AI hurt over " + hurtValue + "%");
		
		parent.ChildDone(this, areYouHurt);
	}

	public GUINode GetView()
	{
		return view;
	}

	public void Delete()
	{
		parent.RemoveChild(this);
	}

	public ParentNode GetParent()
	{
		return parent;
	}

	public int GetTypeID()
	{
		return TypeID;
	}

}
