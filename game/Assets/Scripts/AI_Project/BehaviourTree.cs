using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourTree : MonoBehaviour {

	NodeFactory factory;

	string currentFileName = "no file";

	Behaviour SearchAndAttack;
	GameObject player;
	GameObject ai;
	// Use this for initialization

	public Color color = Color.black;
	public string myString = "FileName without ending";
	public bool groupEnabled;
	public bool myBool = true;
	public float myFloat = 1.23f;
	
	public bool hasBehaviourNode = false;

	Behaviour behaviour;

	List<ChildNode> nodes;

	List<GUINode> guiNodes;

	public void Init()
	{
		if(nodes == null && guiNodes == null)
		{
			Debug.Log ("init nodes");
			nodes = new List<ChildNode>();
			guiNodes = new List<GUINode>();
		}
		if(factory == null)
		{
			factory = new NodeFactory(this);
		}
	}

	void Awake()
	{
		ai = this.gameObject;
		player = GameObject.FindGameObjectWithTag("Player0");
		/*
		SearchAndAttack = new Behaviour();
			Selector sel_main = new Selector(SearchAndAttack);
				Sequence seq_Aggressive = new Sequence(sel_main);
					Selector sel_Hurt = new Selector(seq_Aggressive);
						Inverter not_Hurt = new Inverter(sel_Hurt);
							AreYouHurt hurt = new AreYouHurt(not_Hurt, true);
						IsOpponentMoreHurt oppHurt = new IsOpponentMoreHurt(sel_Hurt, true);
					True true_attack = new True(seq_Aggressive);
						Sequence seq_Attack = new Sequence(true_attack);
							SeekOpponentTask seekOpponent = new SeekOpponentTask(seq_Attack, SearchAndAttack);
							Selector attackOrFlank = new Selector(seq_Attack);
								ParallelOneForAll attackAndMaintain = new ParallelOneForAll(attackOrFlank);
									AttackTask attack = new AttackTask(attackAndMaintain, SearchAndAttack);
									UntilFail loop_dist = new UntilFail(attackAndMaintain);
										MaintainDistanceTask maintain = new MaintainDistanceTask(loop_dist, SearchAndAttack, 9.0f, 2.0f);
								FlankOpponentTask flank_att = new FlankOpponentTask(attackOrFlank, SearchAndAttack, 9.0f);
							//Selector sel_Attack = new Selector(seq_LoopCondition);
				Selector sel_Defensive = new Selector(sel_main);
					ParallelOneForAll healthAndFlee = new ParallelOneForAll(sel_Defensive);
						SeekNearestHealthTask health = new SeekNearestHealthTask(healthAndFlee, SearchAndAttack);
						UntilFail loop_flee = new UntilFail(healthAndFlee);
							EvadeOpponentTask flee = new EvadeOpponentTask(loop_flee, SearchAndAttack);
					
					ParallelOneForAll flankAndFlee2 = new ParallelOneForAll(sel_Defensive);
						FlankOpponentTask flank = new FlankOpponentTask(flankAndFlee2, SearchAndAttack, 81.0f);
						EvadeOpponentTask flee2 = new EvadeOpponentTask(flankAndFlee2, SearchAndAttack);
				//wander around
				*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		SearchAndAttack.Update(); 
	}

	
	void OnGUI ()
	{ 
		GUI.TextArea (new Rect (10, 10, 200, 50), "No. Active Tasks: " + SearchAndAttack.GetActiveTasks().Count
		              + "\nis Running: " + SearchAndAttack.GetIsRunning()); 
	
	}

	public void AddNode(ChildNode n)
	{
		nodes.Add(n);
	}

	public void RemoveNode(ChildNode n)
	{
		nodes.Remove(n);
	}
	
	
	public void RemoveNodeAt(int i)
	{
		nodes.RemoveAt(i);
	}

	public void AddGUINode(GUINode n)
	{
		guiNodes.Add(n);
	}
	
	public void RemoveGUINode(GUINode n)
	{
		guiNodes.Remove(n);
	}

	public List<GUINode> GetGUINodes()
	{
		return guiNodes;
	}

	public void SetBehaviour(Behaviour b)
	{
		behaviour = b;
	}

	public ChildNode GetNodeAt(int i)
	{
		return nodes[i];
	}

	public int GetCount()
	{
		return nodes.Count;
	}

	public Behaviour GetBehaviour()
	{
		return behaviour;
	}

	public NodeFactory GetFactory()
	{
		return factory;
	}
	
	public string GetCurrentFileName()
	{
		return currentFileName;
	}

	public void SetCurrentFileName(string s)
	{
		currentFileName = s;
	}
}
