using UnityEngine;
using System.Collections;

public class HitReach : MonoBehaviour {
	
	private bool isInReach = false;
	private int opposingPlayer;
	private int player;
	private Collider opponentCol;
	private Animator anim;
	

	void Awake()
	{
		PlayerMovement pm = transform.parent.GetComponent<PlayerMovement>();
		player = pm.player;
		if (player == 0)
			opposingPlayer = 1;
		else
			opposingPlayer = 0;
			
		anim = transform.parent.GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetButtonDown("Hit_"+player))
			anim.SetTrigger("Jab");
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player"+opposingPlayer)
		{
			opponentCol = col;
			isInReach = true;
			Debug.Log ("Is In Reach: "+col.tag);
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player"+opposingPlayer)
		{
			isInReach = false;
			Debug.Log ("Out of Reach: "+col.tag);
		}
	}
}
