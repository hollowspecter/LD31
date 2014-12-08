using UnityEngine;
using System.Collections;

public class IceMode : MonoBehaviour {
	
	static bool icemodeOn = false;
	
	PlayerIcemode player0;
	PlayerIcemode player1;
	MeshRenderer floorRenderer;
	public AudioClip pickupSound;
	public float iceModeDuration;
	public static int amountOfSnowflakesForForever = 100;
	
	public Material iceMaterial;
	
	public float fadeSpeed = 1.0f;


	SpriteRenderer render;
	Material originalMaterial;
	
	bool fadeIn = false;
	bool fadeOut = false;
	static bool icemodeForever = false;
	
	float timer = 0.0f;
	
	void Awake()
	{
		floorRenderer = GameObject.FindGameObjectWithTag("Floor").GetComponent<MeshRenderer>();
		
		player0 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerIcemode>();
		if(player0 == null)
			player0 = GameObject.Find("boar").GetComponent<PlayerIcemode>();
		if (player0 == null)
			Debug.LogError("Player 0 is not assigned");
			
		player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerIcemode>();
		if(player1 == null)
			player1 = GameObject.Find("deer").GetComponent<PlayerIcemode>();
		if (player1 == null)
			Debug.LogError("Player 1 is not assigned");
			
		originalMaterial = floorRenderer.material;
		render = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if ((SnowFlake.snowflakeCount > amountOfSnowflakesForForever) && !icemodeForever)
		{
			icemodeForever = true;
			fadeIn = true;
			floorRenderer.material = iceMaterial;
			floorRenderer.material.color = Color.clear;
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
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0") || col.CompareTag("Player1"))
		{
			if (!icemodeOn && !icemodeForever)
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
		floorRenderer.material = iceMaterial;
		floorRenderer.material.color = Color.clear;
		player0.icemode = true;
		player1.icemode = true;
		icemodeOn = true;
		yield return new WaitForSeconds(iceModeDuration);
		
		fadeOut = true;
		if (!icemodeForever)
		{
			floorRenderer.material.color = Color.clear;
			player0.icemode = false;
			player1.icemode = false;
			floorRenderer.material = originalMaterial;
		}
		
	}
}
