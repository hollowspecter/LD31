using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStance : MonoBehaviour {

	public float startingMultiplier = 0.0f;
	public float currentMultiplier;
	public Text multiplierText;
	
	Animator anim;
	PlayerMovement playerMovement;
	bool damaged;
	int player;
	Rigidbody playerRigidbody;
	bool respawning = false;
	
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		currentMultiplier = startingMultiplier;
		player = playerMovement.player;
		playerRigidbody = GetComponent<Rigidbody> ();
		
	}
	
	void Update()
	{
		if (damaged)
		{
			// set something like a flashing thing...
			// or hurt animation
		}
		else
		{
			// make it go away
		}
		damaged = false;
		
		UpdateText();
	}
	
	public void TakeHit(float amount, Vector3 hitposition, string type)
	{
		if (!respawning)
		{
			damaged = true;
			
			currentMultiplier += amount;
			
			Vector3 recoil = transform.position - hitposition;
			if (type == "strong")
				recoil = recoil.normalized * 800 * Time.deltaTime * 10*(1.0f+currentMultiplier);
			else
				recoil = recoil.normalized * 800 * Time.deltaTime * (1.0f+currentMultiplier);
			
			playerRigidbody.AddForce(recoil);
		}
	}
	
	void UpdateText()
	{
		multiplierText.text = "Player"+player+" Factor: "+currentMultiplier;
	}
	
	public void resetMultiplier()
	{
		currentMultiplier = startingMultiplier;
	}
	
	public void startedRespawning()
	{
		respawning = true;
	}
	
	public void endedRespawning()
	{
		respawning = false;
	}
}
