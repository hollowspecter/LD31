using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class env_Bumper : MonoBehaviour {

	public int recoilMul = 0;

	float bumperStrength = 1f;

	PlayerStance player0;
	PlayerStance player1;
	
	Animator anim;

	// Use this for initialization
	void Awake () 
	{
		player0 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerStance>();
		if(player0 == null)
			player0 = GameObject.Find("boar").GetComponent<PlayerStance>();
		player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStance>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter(Collider col)
	{
		if(recoilMul == 0)
		{
			if(col.tag == "Player0")
			{
				anim.SetTrigger("bumped");
				player0.TakeHit(bumperStrength, transform.position, "bumper");
			}
			if(col.tag == "Player1")
			{
				anim.SetTrigger("bumped");
				player0.TakeHit(bumperStrength, transform.position, "bumper");
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
