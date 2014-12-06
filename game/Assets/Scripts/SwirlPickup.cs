using UnityEngine;
using System.Collections;

public class SwirlPickup : MonoBehaviour {
	
	public AudioClip pickupSound;
	public float swirlDuration = 6.0f;
	
	SpriteRenderer render;
	Camera mainCam;
	
	void Awake()
	{
		render = transform.GetChild(0).GetComponent<SpriteRenderer>();
		mainCam = Camera.main;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0") || col.CompareTag("Player1"))
		{
			collider.enabled = false;
			render.color = Color.clear;
			audio.PlayOneShot(pickupSound);
			
			StartCoroutine("swirl");
		}
	}
	
	IEnumerator swirl()
	{
		mainCam.orthographicSize *= -1;
		yield return new WaitForSeconds(swirlDuration);
		mainCam.orthographicSize *= -1;
		Destroy(this);
	}
}