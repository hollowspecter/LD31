using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public int player = 0; // which player is it?

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	// physics update
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal_"+player); //just 1, -1 or 0 snaps!, axis = input
		float v = Input.GetAxisRaw ("Vertical_"+player);
		
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
}
