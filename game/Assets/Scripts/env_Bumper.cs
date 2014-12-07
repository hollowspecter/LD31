using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class env_Bumper : MonoBehaviour {

	//an optional multiplier to use instead of adding a type to playerStance
	public int recoilMul = 0;

	//how much "damage" will the bumper do
	float bumperStrength = 1f;

	PlayerStance player0;
	PlayerStance player1;
	
	Animator anim;

	// Use this for initialization
	void Awake () 
	{
		//find the players (first command encountered some problems)
		player0 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerStance>();
		if(player0 == null)
			player0 = GameObject.Find("boar").GetComponent<PlayerStance>();

		player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStance>();
		if(player1 == null)
			player1 = GameObject.Find("deer").GetComponent<PlayerStance>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame...or not
	void Update ()
	{

	}

	void OnTriggerEnter(Collider col)
	{
		//Which takehit function do you want to use?
		if(recoilMul == 0)
		{
			//which player is in your collider 
			if(col.tag == "Player0")
			{
				anim.SetTrigger("bumped");
				player0.TakeHit(bumperStrength, transform.position, "bumper");
			}
			if(col.tag == "Player1")
			{
				anim.SetTrigger("bumped");
				player1.TakeHit(bumperStrength, transform.position, "bumper");
			}
		}
		else
		{
			if(col.tag == "Player0")
			{
				anim.SetTrigger("bumped");
				player0.TakeHit(bumperStrength, transform.position, recoilMul);
			}
			if(col.tag == "Player1")
			{
				anim.SetTrigger("bumped");
				player1.TakeHit(bumperStrength, transform.position, recoilMul);
			}
		}
	}
}
