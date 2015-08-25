﻿using UnityEngine;
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
					Selector sel_Hurt = new Selector(seq_Aggressive);
						Inverter not_Hurt = new Inverter(sel_Hurt);
							AreYouHurt hurt = new AreYouHurt(not_Hurt, true);
						IsOpponentMoreHurt oppHurt = new IsOpponentMoreHurt(sel_Hurt, true);
					True true_attack = new True(seq_Aggressive);
						Sequence seq_Attack = new Sequence(true_attack);
							SeekOpponentTask seekOpponent = new SeekOpponentTask(seq_Attack, SearchAndAttack);
							Selector attackOrFlank = new Selector(seq_Attack);
								Selector sel_Attack = new Selector(attackOrFlank);
									Sequence seq_Strong = new Sequence(sel_Attack);
										IsOpponentHurt isHeHurt = new IsOpponentHurt(seq_Strong, true);
										ParallelOneForAll strongAndMaintain = new ParallelOneForAll(seq_Strong);
											AttackStrongTask strong = new AttackStrongTask(strongAndMaintain, SearchAndAttack);
											UntilFail loop_dist = new UntilFail(strongAndMaintain);
												MaintainDistanceTask maintain = new MaintainDistanceTask(loop_dist, SearchAndAttack, 9.0f, 2.0f);
									ParallelOneForAll attackAndMaintain = new ParallelOneForAll(sel_Attack);
										AttackTask attack = new AttackTask(attackAndMaintain, SearchAndAttack);
										UntilFail loop_dist2 = new UntilFail(attackAndMaintain);
											MaintainDistanceTask maintain2 = new MaintainDistanceTask(loop_dist2, SearchAndAttack, 9.0f, 2.0f);
								FlankOpponentTask flank_att = new FlankOpponentTask(attackOrFlank, SearchAndAttack, 9.0f);
							
				Selector sel_Defensive = new Selector(sel_main);
					ParallelOneForAll healthAndFlee = new ParallelOneForAll(sel_Defensive);
						SeekNearestHealthTask health = new SeekNearestHealthTask(healthAndFlee, SearchAndAttack);
						UntilFail loop_flee = new UntilFail(healthAndFlee);
							EvadeOpponentTask flee = new EvadeOpponentTask(loop_flee, SearchAndAttack);
					
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
