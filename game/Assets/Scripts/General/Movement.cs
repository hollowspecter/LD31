using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public float speed = 6f;
	public float walkingSpeed = 6f;
	public float dashSpeed = 36f;
	public int player = 0; // which player is it?
	public float forceMultiplier = 10.0f;
	
	[HideInInspector]
	protected Vector3 movement;
	protected Animator anim;
	protected Rigidbody playerRigidbody;
	protected PlayerJab playerAttacks;
	protected RigidbodyConstraints originalCons;
	protected PlayerDeath playerDeath;
	
	public bool blockMovement = false;
	public bool isDashing = false;
	public bool moveByForce = false;
	
	protected bool onFloor = false;
	protected float fallSpeed = 800f;

	protected float dashH = 0f;
	protected float dashV = 0f;
	
	public virtual void Awake()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		playerAttacks = GetComponent<PlayerJab>();
		playerDeath = GetComponentInChildren<PlayerDeath>();
		if (player == 1)
		{
			anim.SetBool ("Right", false);
		}
		
		originalCons = playerRigidbody.constraints;
	}
	
	// physics update
	void FixedUpdate()
	{	

	}
	
	protected void Move (float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		
		if (!moveByForce)
			playerRigidbody.MovePosition (transform.position + movement); //current pos + movement
		else
			playerRigidbody.AddForce (movement * forceMultiplier);
	}
	
	protected void Turning(float h, float v, bool isWalking)
	{
		if (isWalking)
		{
			Vector3 movement = new Vector3(h, 0.0f, v);
			Quaternion newRotation = Quaternion.LookRotation(movement);
			playerRigidbody.MoveRotation(newRotation);
		}
	}
	
	protected void Animating (float h, float v, bool walking)
	{
		anim.SetBool ("IsWalking", walking);
		if (h != 0)
			anim.SetBool ("Right", h > 0);
	}
	
	protected bool getKnockdown()
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(2);
		bool knockdown = info.IsName("knockdownL") || info.IsName("knockdownR");
		bool knockdownIdle = info.IsName("knockdownIdleL") || info.IsName("knockdownIdleR");
		
		return knockdown || knockdownIdle;
	}
	
	protected void OnCollisionStay(Collision col)
	{
		if(col.collider.tag == "Floor")
		{
			onFloor = true;
		}
	}
	
	protected void OnCollisionExit(Collision col)
	{
		//Debug.Log("not on:" + col.collider.tag);
		if(col.collider.tag == "Floor")
		{
			onFloor = false;
		}
	}
	
	public Vector3 getMovement()
	{
		return movement;
	}
	
	protected void Fall()
	{
		playerRigidbody.AddForce(0,-1 * fallSpeed,0);
		originalCons = playerRigidbody.constraints;
		playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

	}
	
	public void PushByForce(float h, float v, float distance)
	{	
		movement.Set (h, 0f, v);
		movement = movement.normalized * distance * Time.deltaTime;
		playerRigidbody.AddForce (movement * forceMultiplier);
	}

	public bool getOnFloor()
	{
		return onFloor;
	}
}
