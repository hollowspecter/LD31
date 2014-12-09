using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSpecial : MonoBehaviour {
	
	public int player = 0;
	public float dashStrength = 2.0f;
	public PlayerStance opponent;
	public float cooldown = 50.0f;
	public float recoverSpeed = 10.0f;
	
	public AudioClip sfx_dashHit;
	
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;
	
	public Slider cooldownSlider;
	
	public AudioClip sfx_dash;
	
	bool hasWeapon = false;
	float weaponStrength;
	AudioClip sfx_weaponHit;
	
	Animator anim;
	HitReach hitreach;
	PlayerMovement self;
	
	bool isJabbing = false;
	bool isStronging = false;
	bool isComboing = false;
	bool isKicking = false;
	
	float timer = 0.0f;
	float maxTimer = 0.2f;
	
	float currentCooldown = 0.0f;
	
	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		hitreach = GetComponentInChildren<HitReach>();
		
		self = GetComponent<PlayerMovement>();
	}
	
	void Update()
	{
		hasWeapon = anim.GetBool ("Gun");
	
		if (!canUseSpecial())
		{
			currentCooldown += recoverSpeed * Time.deltaTime;
		}
		
		cooldownSlider.value = currentCooldown / cooldown;
		
		anim.SetBool("Dash", self.isDashing);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1);
		isJabbing = info.IsName("jabR") || info.IsName("jabL");
		isStronging = info.IsName("strongR") || info.IsName("strongL");
		isComboing = info.IsName("comboR") || info.IsName("comboL");
		isKicking = info.IsName("kickR") || info.IsName("kickL");
		
		if (Input.GetButtonDown("Special_"+player)
			&& !isJabbing
			&& !isStronging
			&& !isComboing
			&& !isKicking
			&& !self.isDashing
			&& !hasWeapon
			&& !self.blockMovement)
		{
			if (canUseSpecial())
			{
				//Debug.Log("Dash!");
				timer = 0.0f;
				self.isDashing = true;
				playSound(sfx_dash)	;
			}			
		}
		
		
		if(self.isDashing)
		{
			// hits player
			if (hitreach.opposingPlayerInReach())
			{
				//Debug.Log("Hit with Dash!");
				opponent.TakeHit(dashStrength, transform.position, "dash");
				playSound(sfx_dashHit);
				
				self.isDashing = false;
				resetCooldown();
			}
			timer += Time.deltaTime;
			
			// timer goes off
			if(timer >= maxTimer)
			{	
				self.isDashing = false;
				resetCooldown();
				//Debug.Log("timeup Dash!");
			}
		}
		
		
	}
	
	void playSound(AudioClip sfx)
	{
		float vol = Random.Range(minVol, maxVol);
		audio.pitch = Random.Range (minPitch, maxPitch);
		audio.PlayOneShot(sfx, vol);
	}
	
	void resetCooldown()
	{
		currentCooldown = 0.0f;
	}
	
	bool canUseSpecial()
	{
		return currentCooldown >= cooldown;
	}
	
	public void recoverCooldown(float rec)
	{
		currentCooldown += rec;
		if (currentCooldown > cooldown)
			currentCooldown = cooldown;
	}
}
