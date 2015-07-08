using UnityEngine;
using System.Collections;

public class SnowCanonPickup : MonoBehaviour {

	public AudioClip pickupSound;
	
	SpriteRenderer render;
	
	void Awake()
	{
		render = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	void OnTriggerEnter(Collider col)
	{
		if ((col.CompareTag("Player0") || col.CompareTag("Player1")))
		{
			Animator anim = col.GetComponent<Animator>();
			
			if (!anim.GetBool("Gun"))
			{
				anim.SetBool("Gun", true);
				
				GetComponent<Collider>().enabled = false;
				render.color = Color.clear;
				GetComponent<AudioSource>().PlayOneShot(pickupSound);
				Destroy(gameObject, pickupSound.length);
			}
		}
	}
}
