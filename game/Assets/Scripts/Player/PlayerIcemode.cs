using UnityEngine;
using System.Collections;

public class PlayerIcemode : MonoBehaviour {

	public bool icemode = false;
	static float iceDrag = 0.5f;
	public bool iceblock = false;
	
	Rigidbody rigid;
	float originalDrag;
	Movement playerMov;
	
	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		playerMov = GetComponent<PlayerMovement>();		
		if(playerMov == null)
			playerMov = GetComponent<AI_Movement>();

		originalDrag = rigid.drag;
	}

	void Update()
	{
		if (icemode || iceblock)
		{
			rigid.drag = iceDrag;
			playerMov.moveByForce = true;
		}
		else if (!icemode && !iceblock)
		{
			rigid.drag = originalDrag;
			playerMov.moveByForce = false;
		}
	}
}
