using UnityEngine;
using System.Collections;

public class IceMode : MonoBehaviour {
	
	public static bool icemodeOn = false;
	
	PlayerMovement player0;
	PlayerMovement player1;
	MeshRenderer floorRenderer;
	public AudioClip pickupSound;
	public float iceModeDuration;
	public static int amountOfSnowflakesForForever = 10;
	
	public float iceDrag = 0.5f;
	public Material iceMaterial;

	public MeshRenderer fakefloor;

	public float fadeSpeed = 1.0f;

	Rigidbody rigid0;
	Rigidbody rigid1;
	float originalDrag;
	Material originalMaterial;
	
	bool fadeIn = false;
	bool fadeOut = false;
	static bool icemodeForever = false;
	
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
	}

	// Update is called once per frame
	void Update ()
	{
		if ((SnowFlake.snowflakeCount > amountOfSnowflakesForForever) && !icemodeForever)
		{
			icemodeForever = true;
			fadeIn = true;
			rigid0.drag = iceDrag;
			rigid1.drag = iceDrag;
			floorRenderer.material = iceMaterial;
			floorRenderer.material.color = new Color(0.027f,0.191f,0.52f,0f);
			player0.moveByForce = true;
			player1.moveByForce = true;
			icemodeOn = true;
			Debug.Log ("Ice mode forever!");
		}
			
	
		if (fadeIn && !fadeOut)
		{
			floorRenderer.material.color = Color.Lerp(floorRenderer.material.color, Color.white, fadeSpeed * Time.deltaTime);
		}
		else if (fadeOut)
		{
			timer += Time.deltaTime;
			if (!icemodeForever)
				floorRenderer.material.color = Color.Lerp(floorRenderer.material.color, Color.white, fadeSpeed * Time.deltaTime);
			if (timer >= 1.5f)
			{
				if (!icemodeForever)
					icemodeOn = false;
				Destroy(gameObject);
			}
		}
	}
	/*
	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player0") || col.CompareTag("Player1"))
		{
			if (!icemodeOn && !icemodeForever)
			{
				collider.enabled = false;
				render.color = Color.clear;
				audio.PlayOneShot(pickupSound);
				
				StartCoroutine("iceMode");
			}
		}
	}*/

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
		if (!icemodeForever)
		{
			floorRenderer.material.color = Color.clear;
			rigid0.drag = originalDrag;
			rigid1.drag = originalDrag;
			player0.moveByForce = false;
			player1.moveByForce = false;
			floorRenderer.material = originalMaterial;
		}
		
	}
}
