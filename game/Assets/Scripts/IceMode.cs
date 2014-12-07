using UnityEngine;
using System.Collections;

public class IceMode : MonoBehaviour {
	
	static bool icemodeOn = false;
	
	PlayerMovement player0;
	PlayerMovement player1;
	MeshRenderer floorRenderer;
	public AudioClip pickupSound;
	public float iceModeDuration;
	
	public float iceDrag = 0.5f;
	public Material iceMaterial;
	
	public float fadeSpeed = 1.0f;

	Rigidbody rigid0;
	Rigidbody rigid1;
	float originalDrag;
	SpriteRenderer render;
	Material originalMaterial;
	
	bool fadeIn = false;
	bool fadeOut = false;
	
	float timer = 0.0f;
	
	void Awake()
	{
		floorRenderer = GameObject.FindGameObjectWithTag("Floor").GetComponent<MeshRenderer>();
		player0 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerMovement>();
		player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovement>();
		rigid0 = player0.rigidbody;
		rigid1 = player1.rigidbody;
		originalDrag = rigid0.drag;
		originalMaterial = floorRenderer.material;
		render = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if (fadeIn && !fadeOut)
		{
			floorRenderer.material.color = Color.Lerp(floorRenderer.material.color, Color.white, fadeSpeed * Time.deltaTime);
		}
		else if (fadeOut)
		{
			timer += Time.deltaTime;
			floorRenderer.material.color = Color.Lerp(floorRenderer.material.color, Color.white, fadeSpeed * Time.deltaTime);
			if (timer >= 1.5f)
			{
				icemodeOn = false;
				Destroy(gameObject);
			}
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0") || col.CompareTag("Player1"))
		{
			if (!icemodeOn)
			{
				collider.enabled = false;
				render.color = Color.clear;
				audio.PlayOneShot(pickupSound);
				
				StartCoroutine("iceMode");
			}
		}
	}
	
	IEnumerator iceMode()
	{
		fadeIn = true;
		rigid0.drag = iceDrag;
		rigid1.drag = iceDrag;
		floorRenderer.material = iceMaterial;
		floorRenderer.material.color = Color.clear;
		player0.moveByForce = true;
		player1.moveByForce = true;
		icemodeOn = true;
		yield return new WaitForSeconds(iceModeDuration);
		
		fadeOut = true;
		floorRenderer.material.color = Color.clear;
		rigid0.drag = originalDrag;
		rigid1.drag = originalDrag;
		player0.moveByForce = false;
		player1.moveByForce = false;
		floorRenderer.material = originalMaterial;
	}
}
