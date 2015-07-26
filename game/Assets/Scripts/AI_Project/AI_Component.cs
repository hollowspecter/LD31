using UnityEngine;
using System.Collections;

public class AI_Component : MonoBehaviour {

	Behaviour SearchAndAttack;
	GameObject player;
	GameObject ai;
	// Use this for initialization


	void Awake()
	{
		ai = this.gameObject;
		player = GameObject.FindGameObjectWithTag("Player0");

		SearchAndAttack = new Behaviour();
			Selector sel_main = new Selector(SearchAndAttack);	
				Sequence seq_Aggressive = new Sequence(sel_main);
					Inverter not_Hurt = new Inverter(seq_Aggressive);
						AreYouHurt_C hurt = new AreYouHurt_C(not_Hurt);
					SeekOpponentTask seekOpponent = new SeekOpponentTask(seq_Aggressive, SearchAndAttack);
					Selector attackOrFlank = new Selector(seq_Aggressive);
						AttackTask attack = new AttackTask(attackOrFlank, SearchAndAttack);
						FlankOpponentTask flank_att = new FlankOpponentTask(attackOrFlank, SearchAndAttack, 9.0f);
					//Selector sel_Attack = new Selector(seq_LoopCondition);
				Selector sel_Defensive = new Selector(sel_main);
					ParallelOneForAll healthAndFlee = new ParallelOneForAll(sel_Defensive);
						SeekNearestHealthTask health = new SeekNearestHealthTask(healthAndFlee, SearchAndAttack);
						EvadeOpponentTask flee = new EvadeOpponentTask(healthAndFlee, SearchAndAttack);
					
					ParallelOneForAll flankAndFlee2 = new ParallelOneForAll(sel_Defensive);
						FlankOpponentTask flank = new FlankOpponentTask(flankAndFlee2, SearchAndAttack, 81.0f);
						EvadeOpponentTask flee2 = new EvadeOpponentTask(flankAndFlee2, SearchAndAttack);
				//wander around
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
}
