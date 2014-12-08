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
	
		if (Input.GetButtonDown("Hit_"+player) && !isJabbing && !isStronging && !isShooting && !pm.blockMovement)
			anim.SetTrigger("Jab");
			
		if (Input.GetButtonDown("Strong_"+player) && !isJabbing && !isStronging && !isShooting && !pm.blockMovement)
			anim.SetTrigger("Strong");
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
		audio.pitch = Random.Range (minPitch, maxPitch);
		audio.PlayOneShot(sfx, vol);
	}
	
	public bool getIsStronging()
	{
		return isStronging;
	}
}
