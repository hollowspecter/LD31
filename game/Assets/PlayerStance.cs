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
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		currentMultiplier = startingMultiplier;
		player = playerMovement.player;
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
	
	public void TakeHit(float amount)
	{
		damaged = true;
		
		currentMultiplier += amount;
	}
	
	void UpdateText()
	{
		multiplierText.text = "Player"+player+" Factor: "+currentMultiplier;
	}
}
