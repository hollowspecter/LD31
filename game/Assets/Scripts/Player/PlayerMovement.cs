using UnityEngine;

public class PlayerMovement : Movement
{

	public override void Awake()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		playerAttacks = GetComponent<PlayerJab>();
		if (player == 1)
		{
			anim.SetBool ("Right", false);
		}

		originalCons = playerRigidbody.constraints;
	}

	// physics update
	void FixedUpdate()
	{	
		playerRigidbody.constraints = originalCons;
		speed = walkingSpeed;
		float h = Input.GetAxisRaw ("Horizontal_"+player); //just 1, -1 or 0 snaps!, axis = input
		float v = Input.GetAxisRaw ("Vertical_"+player);
		
		if (playerAttacks.getIsStronging() || blockMovement
			|| getKnockdown())
		{
			h = 0;
			v = 0;
		}
		
		if(isDashing)
		{
			speed = dashSpeed;
			h = dashH;
			v = dashV;
		}
		else
		{
			dashH = h;
			dashV = v;
		}
		
		bool walking = h != 0f || v != 0f;
		if(onFloor)
		{
			Move (h, v);
			Turning (h, v, walking);
			Animating (h, v, walking);
		}
		else if(!blockMovement)
		{
			//Debug.Log("fall");
			Fall();
		}
	}
}
