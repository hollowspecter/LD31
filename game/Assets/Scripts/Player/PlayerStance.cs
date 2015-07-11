using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStance : MonoBehaviour {

	public float startingMultiplier = 0.0f;
	public float currentMultiplier;
	public Text multiplierText;
	public float knockdownProbabilityFactor = 3000.0f;

	Animator anim;
	//PlayerMovement playerMovement;
	bool damaged;
	//int player;
	Rigidbody playerRigidbody;
	bool respawning = false;

	
	void Awake()
	{
		anim = GetComponent<Animator>();
		//playerMovement = GetComponent<PlayerMovement>();
		currentMultiplier = startingMultiplier;
		//player = playerMovement.player;
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
		//Debug.Log ("tookHit: " + amount);
		if (!respawning)
		{
			damaged = true;
			
			currentMultiplier += amount;
			
			Vector3 recoil = transform.position - hitposition;

			if (type == "strong")
				recoil = recoil.normalized * 800 * Time.deltaTime * 5 *(1.0f+currentMultiplier);
			else if (type == "dash")
				recoil = recoil.normalized * 800 * Time.deltaTime * 7 *(1.0f+currentMultiplier);
			else if (type == "bumper")
				recoil = recoil.normalized * 800 * Time.deltaTime * 10 *(1.0f+(currentMultiplier)<5f?5f:currentMultiplier);
			else
				recoil = recoil.normalized * 800 * Time.deltaTime * (1.0f+currentMultiplier);

            //Debug.Log("Recoil: " + recoil);
            
			playerRigidbody.AddForce(new Vector3(recoil.x,0.0f,recoil.z));
			
			// knockdown
			if (calculateKnockdownProbabilty(recoil.magnitude))
			{
				anim.SetTrigger("Knockdown");
			}
		}
	}

	public void TakeHit(float amount, Vector3 hitposition, int recoilMul/*set to 1 to ignore*/)
	{
		if (!respawning)
		{
			damaged = true;
			
			currentMultiplier += amount;
			
			Vector3 recoil = transform.position - hitposition;

			recoil = recoil.normalized * 800 * Time.deltaTime * recoilMul *(1.0f+currentMultiplier);
			
			playerRigidbody.AddForce(recoil);
			
			// knockdown
			if (calculateKnockdownProbabilty(recoil.magnitude))
			{
				anim.SetTrigger("Knockdown");
			}
		}
	}
	
	void UpdateText()
	{
		multiplierText.text = currentMultiplier+"%";
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
	
	public void reduceMultiplier(float red)
	{
		currentMultiplier -= red;
		if (currentMultiplier < 0)
			currentMultiplier = 0;
	}
	
	bool calculateKnockdownProbabilty(float magnitude)
	{
		float rndm = Random.value;
		
		float probability = (1.0f / knockdownProbabilityFactor) * magnitude;
		
		return rndm < probability;
	}
}
