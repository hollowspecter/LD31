using UnityEngine;
using System.Collections;

public class HitReach : MonoBehaviour {
	
	private bool isInReach = false;
	private int opposingPlayer;
	private Collider opponentCol;
	
	void Awake()
	{
		PlayerMovement pm = transform.parent.GetComponent<PlayerMovement>();
		if (pm.player == 0)
			opposingPlayer = 1;
		else
			opposingPlayer = 0;
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
