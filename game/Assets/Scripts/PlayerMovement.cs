using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float walkingSpeed = 6f;
	public float dashSpeed = 36f;
	public int player = 0; // which player is it?

	[HideInInspector]
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	PlayerJab playerAttacks;
	
	public bool blockMovement = false;
	public bool isDashing = false;
	
	float dashH = 0f;
	float dashV = 0f;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		playerAttacks = GetComponent<PlayerJab>();
		if (player == 1)
		{
			anim.SetBool ("Right", false);
		}
	}

	// physics update
	void FixedUpdate()
	{
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
		
		Move (h, v);
		Turning (h, v, walking);
		Animating (h, v, walking);
	}

	void Move (float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement); //current pos + movement
	}

	void Turning(float h, float v, bool isWalking)
	{
		if (isWalking)
		{
			Vector3 movement = new Vector3(h, 0.0f, v);
			Quaternion newRotation = Quaternion.LookRotation(movement);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating (float h, float v, bool walking)
	{
		anim.SetBool ("IsWalking", walking);
		if (h != 0)
			anim.SetBool ("Right", h > 0);
	}
	
	bool getKnockdown()
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(2);
		bool knockdown = info.IsName("knockdownL") || info.IsName("knockdownR");
		bool knockdownIdle = info.IsName("knockdownIdleL") || info.IsName("knockdownIdleR");
		
		return knockdown || knockdownIdle;
	}
	
	public Vector3 getMovement()
	{
		return movement;
	}
}
