using UnityEngine;
using System.Collections;

public class PlayerSpecial : MonoBehaviour {
	
	public int player = 0;
	public float dashStrength = 2.0f;
	public PlayerStance opponent;
	
	public AudioClip sfx_dashHit;
	
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;
	
	
	bool hasWeapon = false;
	float weaponStrength;
	AudioClip sfx_weaponHit;
	
	Animator anim;
	HitReach hitreach;
	PlayerMovement self;
	
	bool isJabbing = false;
	bool isStronging = false;
	
	float timer = 0.0f;
	float maxTimer = 0.2f;
	
	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		hitreach = GetComponentInChildren<HitReach>();
		
		self = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1);
		isJabbing = info.IsName("jabR") || info.IsName("jabL");
		isStronging = info.IsName("strongR") || info.IsName("strongL");
		
		if (Input.GetButtonDown("Special_"+player) && !isJabbing && !isStronging && !self.isDashing && !hasWeapon )
		{
			Debug.Log("Dash!");
			timer = 0.0f;
			self.isDashing = true;				
		}
		
		
		if(self.isDashing)
		{
			if (hitreach.opposingPlayerInReach())
			{
				Debug.Log("Hit with Dash!");
				opponent.TakeHit(dashStrength, transform.position, "dash");
				playSound(sfx_dashHit);
				
				self.isDashing = false;
				
			}
			timer += Time.deltaTime;
			
			if(timer >= maxTimer)
			{	
				self.isDashing = false;
				Debug.Log("timeup Dash!");
			}
		}
		
		
	}
	
	void playSound(AudioClip sfx)
	{
		float vol = Random.Range(minVol, maxVol);
		audio.pitch = Random.Range (minPitch, maxPitch);
		audio.PlayOneShot(sfx, vol);
	}
	
	
}
