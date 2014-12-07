using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class env_Bumper : MonoBehaviour {

	public int recoilMul = 0;

	float bumperStrength = 1f;

	PlayerStance player0;
	PlayerStance player1;

	bool player0bumped = false;
	bool player1bumped = false;

	// Use this for initialization
	void Awake () 
	{
		player0 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerStance>();
		player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStance>();
	}
	
	// Update is called once per frame
	void Update ()
	{
			
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("enter");
		if(recoilMul == 0)
		{
			if(col.tag == "Player0"&& !player0bumped)
			{	
				player0bumped = true;
				player0.TakeHit(bumperStrength, transform.position, "bumper");
			}
			if(col.tag == "Player1" && !player1bumped)
			{
				player1bumped = true;
				player1.TakeHit(bumperStrength, transform.position, "bumper");
			}
		}
		else
		{
			if(col.tag == "Player0"&& !player0bumped)
			{
				player0bumped = false;
				player0.TakeHit(bumperStrength, transform.position, "bumper");
			}
			if(col.tag == "Player1"&& !player1bumped)
			{
				player1bumped = true;
				player1.TakeHit(bumperStrength, transform.position, "bumper");
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Player0")
		{
			player0bumped = false;
		}
		if(col.tag == "Player1")
		{
			player1bumped = false;
		}
	}
}
