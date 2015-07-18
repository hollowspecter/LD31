using UnityEngine;
using System.Collections;

public class AI_Component : MonoBehaviour {

	Behaviour SearchAndAttack;
	GameObject player;
	GameObject ai;
	// Use this for initialization
	void Start () 
	{
		ai = this.gameObject;
		player = GameObject.FindGameObjectWithTag("Player0");

		SearchAndAttack = new Behaviour();
			UntilFail startLoop = new UntilFail(SearchAndAttack);
				Sequence seq_LoopCondition = new Sequence(startLoop);
					IsOpponentDead_C enemyDead = new IsOpponentDead_C(seq_LoopCondition, player.GetComponent<PlayerMovement>());
					Inverter not_Hurt = new Inverter(seq_LoopCondition);
						AreYouHurt_C hurt = new AreYouHurt_C(not_Hurt, ai.GetComponent<PlayerStance>());
					SeekOpponentTask seekOpponent = new SeekOpponentTask(seq_LoopCondition, SearchAndAttack);
					Selector sel_Attack = new Selector(seq_LoopCondition);
						
					
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
