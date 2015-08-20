using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BehaviourTree : MonoBehaviour {

	NodeFactory factory;

	[SerializeField]
	string currentFilePath = "";

	bool noFileError = false;

	GameObject player;
	GameObject ai;

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

		Init();

		//Load most recent Behaviour Tree
		if(!(currentFilePath == ""))
		{
			factory.ReadFromPath(currentFilePath);
		}
		else
		{
			noFileError = true;
		}
		//Old handmade behaviour Tree
		/*
		behaviour = new Behaviour();
			Selector sel_main = new Selector(behaviour);
				Sequence seq_Aggressive = new Sequence(sel_main);
					Selector sel_Hurt = new Selector(seq_Aggressive);
						Inverter not_Hurt = new Inverter(sel_Hurt);
							AreYouHurt hurt = new AreYouHurt(not_Hurt, true);
						IsOpponentMoreHurt oppHurt = new IsOpponentMoreHurt(sel_Hurt, true);
					True true_attack = new True(seq_Aggressive);
						Sequence seq_Attack = new Sequence(true_attack);
							SeekOpponentTask seekOpponent = new SeekOpponentTask(seq_Attack, behaviour);
							Selector attackOrFlank = new Selector(seq_Attack);
								ParallelOneForAll attackAndMaintain = new ParallelOneForAll(attackOrFlank);
									AttackTask attack = new AttackTask(attackAndMaintain, behaviour);
									UntilFail loop_dist = new UntilFail(attackAndMaintain);
										MaintainDistanceTask maintain = new MaintainDistanceTask(loop_dist, behaviour, 9.0f, 2.0f);
								FlankOpponentTask flank_att = new FlankOpponentTask(attackOrFlank, behaviour, 9.0f);
							//Selector sel_Attack = new Selector(seq_LoopCondition);
				Selector sel_Defensive = new Selector(sel_main);
					ParallelOneForAll healthAndFlee = new ParallelOneForAll(sel_Defensive);
						SeekNearestHealthTask health = new SeekNearestHealthTask(healthAndFlee, behaviour);
						UntilFail loop_flee = new UntilFail(healthAndFlee);
							EvadeOpponentTask flee = new EvadeOpponentTask(loop_flee, behaviour);
					
					ParallelOneForAll flankAndFlee2 = new ParallelOneForAll(sel_Defensive);
						FlankOpponentTask flank = new FlankOpponentTask(flankAndFlee2, behaviour, 81.0f);
						EvadeOpponentTask flee2 = new EvadeOpponentTask(flankAndFlee2, behaviour);
				//wander around
				*/
	}

	// Update is called once per frame
	void Update () 
	{
		if(behaviour != null)
			behaviour.Update(); 
	}

	
	void OnGUI ()
	{ 
		if(noFileError)
		{
			if(EditorUtility.DisplayDialog("No BehaviourTree Error", "You have no Behaviour Tree selected in the Inspector!\n" + 
			                            "Go Back to the Inspector and Press \"Edit Behaviour Tree\"!", "Ok"))
				Application.Quit();
				UnityEditor.EditorApplication.isPlaying = false;
				noFileError = false;
		}
		GUI.TextArea (new Rect (10, 10, 200, 50), "No. Active Tasks: " + behaviour.GetActiveTasks().Count
		              + "\nis Running: " + behaviour.GetIsRunning()); 

	
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

	public void ClearNodes()
	{
		nodes.Clear();
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
	
	public string GetCurrentFilePath()
	{
		return currentFilePath;
	}

	public void SetCurrentFilePath(string s)
	{
		currentFilePath = s;
	}
}
