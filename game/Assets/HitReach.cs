using UnityEngine;
using System.Collections;

public class HitReach : MonoBehaviour {
	
	private bool isInReach = false;
	private int opposingPlayer;
	private int player;
	

	void Awake()
	{
		PlayerMovement pm = transform.parent.GetComponent<PlayerMovement>();
		player = pm.player;
		if (player == 0)
			opposingPlayer = 1;
		else
			opposingPlayer = 0;
	}
	

	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player"+opposingPlayer)
		{
			isInReach = true;
			//Debug.Log ("Is In Reach: "+col.tag);
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player"+opposingPlayer)
		{
			isInReach = false;
			//Debug.Log ("Out of Reach: "+col.tag);
		}
	}
	
	public bool opposingPlayerInReach()
	{
		return isInReach;
	}
}
