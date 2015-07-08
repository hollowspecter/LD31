using UnityEngine;
using System.Collections;

public class HeartPickup : MonoBehaviour {

	public float strength = 5.0f;
	public AudioClip pickupSound;
	
	SpriteRenderer render;
	
	void Awake()
	{
		render = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0") || col.CompareTag("Player1"))
		{
			PlayerStance stance = col.GetComponent<PlayerStance>();
			stance.reduceMultiplier(strength);
			GetComponent<Collider>().enabled = false;
			render.color = Color.clear;
			GetComponent<AudioSource>().PlayOneShot(pickupSound);
			Destroy(gameObject, pickupSound.length);
		}
	}
}
