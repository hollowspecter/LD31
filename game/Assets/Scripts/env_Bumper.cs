using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class env_Bumper : MonoBehaviour {

	public int recoilMul = 0;

	float bumperStrength = 1f;

	PlayerStance player0;
	PlayerStance player1;

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
		if(recoilMul == 0)
		{
			if(col.tag == "Player0")
			{
				player0.TakeHit(bumperStrength, transform.position, "bumper");
			}
			if(col.tag == "Player1")
			{
				player1.TakeHit(bumperStrength, transform.position, "bumper");
			}
		}
		else
		{
			if(col.tag == "Player0")
			{
				player0.TakeHit(bumperStrength, transform.position, "bumper");
			}
			if(col.tag == "Player1")
			{
				player1.TakeHit(bumperStrength, transform.position, "bumper");
			}
		}
	}
}
