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
						FlankOpponentTask flank_att = new FlankOpponentTask(attackOrFlank, SearchAndAttack);
					//Selector sel_Attack = new Selector(seq_LoopCondition);
				Sequence seq_Defensive = new Sequence(sel_main);
					UntilFail healthloop = new UntilFail(seq_Defensive);
						Parallel healthAndFlee = new Parallel(healthloop);
							SeekNearestHealthTask health = new SeekNearestHealthTask(healthAndFlee, SearchAndAttack);
							EvadeOpponentTask flee = new EvadeOpponentTask(healthAndFlee, SearchAndAttack);
					Parallel flankAndFlee2 = new Parallel(seq_Defensive);
						FlankOpponentTask flank = new FlankOpponentTask(flankAndFlee2, SearchAndAttack);
						EvadeOpponentTask flee2 = new EvadeOpponentTask(flankAndFlee2, SearchAndAttack);

		SearchAndAttack.StartBehaviour();

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
