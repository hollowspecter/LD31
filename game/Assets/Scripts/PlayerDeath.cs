using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour {
	
	public int lifes = 5;
	public Text wintext;
	public int player;
	public HeartsView hearts;
	public Transform respawn;
	public float flashingDuration = 4.0f;
	public AudioClip death;
	
	int opposingPlayer;
	PlayerStance stance;
	SpriteRenderer render;
	Color originalColor;
	
	float timer = 0.0f;
	
	bool ending =false;

	void Awake()
	{
		stance = transform.parent.GetComponent<PlayerStance>();
		render = GetComponent<SpriteRenderer>();
		originalColor = render.color;
		
		if (player == 0)
			opposingPlayer = 1;
		else
			opposingPlayer = 0;
	}
	
	void Update()
	{
		if (ending)
		{
			if (Input.GetButtonDown("Submit"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void OnBecameInvisible()
	{	
		//Debug.Log("became Invisible");
		if (lifes > 0) {
			lifes--;
			hearts.reduceHeart(lifes);
			audio.PlayOneShot(death);
			Respawn ();
			StartCoroutine("Flashing");
		}
		else
			Ending ();
	}
	
	void Ending() {
		wintext.text = "Player"+opposingPlayer+"\nWINS!!!\n\nPress Start to Play";
		ending = true;
	}	
	
	void Respawn()
	{
		//Debug.Log ("Respawning");
		transform.parent.position = respawn.position;
		transform.parent.rotation = Quaternion.identity;
		transform.parent.rigidbody.velocity = Vector3.zero;
		transform.parent.rigidbody.angularVelocity = Vector3.zero;
		
		stance.resetMultiplier();
	}
	
	IEnumerator Flashing() {
		stance.startedRespawning();
		
		while (timer < flashingDuration)
		{
			if (render.color == Color.clear)
			{
				render.color = originalColor;
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				render.color = Color.clear;
				yield return new WaitForSeconds(0.1f);
			}
			timer += 0.1f;
		}
		render.color = originalColor;
		timer = 0.0f;
		
		stance.endedRespawning();
	}
}
