using UnityEngine;
using System.Collections;

public class IsOpponentMoreHurt : ConditionNode 
{
	public static int TypeID = 16;
	
	ParentNode parent;

	GUIIsOpponentMoreHurt view;

	PlayerStance ownStance;
	PlayerStance oppStance;
	
	float hurtValue = 33.0f;
	bool randomize = false;
	
	public IsOpponentMoreHurt(ParentNode p, bool randomize, GUIIsOpponentMoreHurt view)
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
		GameObject opponent = GameObject.FindGameObjectWithTag("Player0");
		if(opponent == null)
		{
			opponent = GameObject.Find("boar");
		}

		oppStance = opponent.GetComponent<PlayerStance>();
		
		this.randomize = randomize;
	}

	public IsOpponentMoreHurt()
	{
		Debug.Log ("standard constructor called");
	}
	
	public void Create(int nodeID, Vector2 guiPosition, ParentNode parent, BehaviourTree tree)
	{
		Debug.Log ("Create IsOpponentMoreHurt");
		GUIIsOpponentMoreHurt gui = new GUIIsOpponentMoreHurt(nodeID, guiPosition);
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
		GameObject opponent = GameObject.FindGameObjectWithTag("Player0");
		if(opponent == null)
		{
			opponent = GameObject.Find("boar");
		}
		
		oppStance = opponent.GetComponent<PlayerStance>();
		
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
		Debug.Log ("is the Opponent more hurt?");
		float ownHurt = ownStance.currentMultiplier;
		float oppHurt = oppStance.currentMultiplier;
		
		if(randomize)
		{
			ownHurt += Random.Range(-ownHurt/3, ownHurt/3);
			oppHurt += Random.Range(-oppHurt/3, oppHurt/3);
		}
		bool playerMore = (oppHurt > ownHurt);
		if(playerMore)
			Debug.Log("Player hurt more than AI");
		
		parent.ChildDone(this, playerMore);
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
