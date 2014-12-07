using UnityEngine;
using System.Collections;

public class ApplePickup : MonoBehaviour {

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
			PlayerSpecial special = col.GetComponent<PlayerSpecial>();
			special.recoverCooldown(strength);
			
			collider.enabled = false;
			render.color = Color.clear;
			audio.PlayOneShot(pickupSound);
			Destroy(gameObject, pickupSound.length);
		}
	}
}
