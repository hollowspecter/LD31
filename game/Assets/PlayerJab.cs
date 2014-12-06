using UnityEngine;
using System.Collections;

public class PlayerJab : MonoBehaviour {
	
	public int player = 0;
	public float strength = 1.0f;
	public PlayerStance opponent;
	public AudioClip sfx_goodhit;
	
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;
	
	Animator anim;
	HitReach hitreach;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		hitreach = GetComponentInChildren<HitReach>();
		
	}

	void Update()
	{
		if (Input.GetButtonDown("Hit_"+player))
			anim.SetTrigger("Jab");
	}
	
	public void AttemptedHit()
	{
		// is opposing player in reach?
		if (hitreach.opposingPlayerInReach())
		{
			//Debug.Log("Landed a HIT!");
			opponent.TakeHit(strength, transform.position);
			playSound(sfx_goodhit);
		}
	}
	
	void playSound(AudioClip sfx)
	{
		float vol = Random.Range(minVol, maxVol);
		audio.pitch = Random.Range (minPitch, maxPitch);
		audio.PlayOneShot(sfx, vol);
	}
}
