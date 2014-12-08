using UnityEngine;
using System.Collections;

public class ZoomPickup : MonoBehaviour {
	
	static bool zooming = false;
	
	public AudioClip pickupSound;
	public float zoomDuration = 6.0f;
	public float zoomedCameraSize = 10.0f;
	public float zoomInSpeed = 1.0f;
	public float zoomOutSpeed = 1.0f;
	
	SpriteRenderer render;
	Camera mainCam;
	float originalCamSize;
	
	bool zoomIn = false;
	bool zoomOut = false;
	
	void Awake()
	{
		
		render = transform.GetChild(0).GetComponent<SpriteRenderer>();
		mainCam = Camera.main;
		originalCamSize = 20;
	}
	
	void Update()
	{
		if (zoomIn && !zoomOut)
		{
			mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, zoomedCameraSize, zoomInSpeed * Time.deltaTime);
		}
		else if (zoomOut)
		{
			mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, 14, zoomOutSpeed * Time.deltaTime);
			
			if ((originalCamSize - mainCam.orthographicSize) < 0.05f)
			{
				zooming = false;
				Destroy(gameObject) ;
			}

		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0") || col.CompareTag("Player1"))
		{
			if (!zooming)
			{
				collider.enabled = false;
				render.color = Color.clear;
				audio.PlayOneShot(pickupSound);
				zooming = true;
				
				StartCoroutine("zoomingShit");
			}
		}
	}
	
	IEnumerator zoomingShit()
	{
		zoomIn = true;
		yield return new WaitForSeconds(zoomDuration);
		zoomOut = true;
	}
}