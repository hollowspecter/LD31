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
			//UntilFail startLoop = new UntilFail(SearchAndAttack);
				Sequence seq_LoopCondition = new Sequence(SearchAndAttack);
					//Inverter not_enemyDead = new Inverter(seq_LoopCondition);
					//	IsOpponentDead_C enemyDead = new IsOpponentDead_C(not_enemyDead);
					Inverter not_Hurt = new Inverter(seq_LoopCondition);
						AreYouHurt_C hurt = new AreYouHurt_C(not_Hurt);
					SeekOpponentTask seekOpponent = new SeekOpponentTask(seq_LoopCondition, SearchAndAttack);
					Selector attackOrFlank = new Selector(seq_LoopCondition);
						//AttackTask attack = new AttackTask(attackOrFlank, SearchAndAttack);
						FlankOpponentTask flank = new FlankOpponentTask(attackOrFlank, SearchAndAttack);
					//Selector sel_Attack = new Selector(seq_LoopCondition);
					
		SearchAndAttack.StartBehaviour();
	}
	
	// Update is called once per frame
	void Update () 
	{
		SearchAndAttack.Update(); 
	}

	
	void OnGUI ()
	{ 
		GUI.TextArea (new Rect (10, 10, 200, 50), "No. Active Tasks: " + SearchAndAttack.GetActiveTasks().Count); 
	
	}
}
