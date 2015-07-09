using UnityEngine;
using System.Collections;

public class PlayerJab : MonoBehaviour {
	
	public int player = 0;
	public float jabStrength = 1.0f;
	public float strongStrength = 0.0f;
	public PlayerStance opponent;
	public AudioClip sfx_goodhit;
	public AudioClip sfx_stronghit;
	
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;
	
	Animator anim;
	HitReach hitreach;
	PlayerMovement pm;
	
	private bool isJabbing = false;
	private bool isStronging = false;
	private bool isShooting = false;
	private bool isComboing = false;
	private bool isKicking = false;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		hitreach = GetComponentInChildren<HitReach>();
		pm = GetComponent<PlayerMovement>();
	}

	void Update()
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1);
		isJabbing = info.IsName("jabR") || info.IsName("jabL");
		isStronging = info.IsName("strongR") || info.IsName("strongL");
		isShooting = info.IsName("shootR") || info.IsName("shootL");
		isComboing = info.IsName ("comboR") || info.IsName ("comboL");
		isKicking = info.IsName ("kickR") || info.IsName ("kickL");
			
		if (Input.GetButtonDown("Hit_"+player) && !isStronging && !isComboing && !isKicking && !isShooting && !pm.blockMovement)
		{
			if(isJabbing)
				anim.SetTrigger("Combo");
			else
				anim.SetTrigger("Jab");
		}

		isComboing = info.IsName ("comboR") || info.IsName("comboL");

		if (Input.GetButtonDown("Strong_"+player) && !isJabbing && !isStronging && !isKicking && !isShooting && !pm.blockMovement)
		{
			if(isComboing)
				anim.SetTrigger("Kick");
			else
				anim.SetTrigger("Strong");
		}
	}
	
	public void AttemptedHit()
	{
		// is opposing player in reach?
		if (hitreach.opposingPlayerInReach())
		{
			//Debug.Log("Landed a HIT!");
			opponent.TakeHit(jabStrength, transform.position, "jab");
			playSound(sfx_goodhit);
		}
	}
	
	public void AttemptedStrongHit()
	{
		// is opposing player in reach?
		if (hitreach.opposingPlayerInReach())
		{
			//Debug.Log("Landed a HIT!");
			opponent.TakeHit(strongStrength, transform.position, "strong");
			playSound(sfx_stronghit);
		}
	}
	
	void playSound(AudioClip sfx)
	{
		float vol = Random.Range(minVol, maxVol);
		GetComponent<AudioSource>().pitch = Random.Range (minPitch, maxPitch);
		GetComponent<AudioSource>().PlayOneShot(sfx, vol);
	}
	
	public bool getIsStronging()
	{
		return isStronging;
	}
	
	public void kickJump()
	{
		GetComponent<PlayerMovement>().PushByForce(transform.forward.x, transform.forward.z, 200f);
	}
	
	public void AttemptedKick()
	{
		// is opposing player in reach?
		if (hitreach.opposingPlayerInReach())
		{
			//Debug.Log("Landed a HIT!");
			opponent.TakeHit(strongStrength, transform.position, "strong");
			playSound(sfx_stronghit);
		}
	}
}
