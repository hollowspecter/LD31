using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour {

	public float fallspeed = 80.0f;
	public AudioClip fall;
	
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;

	RigidbodyConstraints originalCons;
	Transform playerT0;
	Transform playerT1;
	Rigidbody rigid0;
	Rigidbody rigid1;
	bool captured0 = false;
	bool captured1 = false;

	void Update()
	{
		if (captured0)
		{
			rigid0.velocity = new Vector3(0,-1 * fallspeed,0);
			
			originalCons = rigid0.constraints;
			rigid0.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			rigid0.GetComponent<Collider>().isTrigger = true;
		}
		if (captured1)
		{
			rigid1.velocity = new Vector3(0,-1 * fallspeed,0);
			
			originalCons = rigid1.constraints;
			rigid1.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			rigid1.GetComponent<Collider>().isTrigger = true;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log("pitfalled");
		if (col.CompareTag("Player0"))
		{
			playerT0 = col.transform;
			rigid0 = playerT0.GetComponent<Rigidbody>();

			captured0 = true;
			playSound(fall);
		}
		if (col.CompareTag("Player1"))
		{
			playerT1 = col.transform;
			rigid1 = playerT1.GetComponent<Rigidbody>();

			captured1 = true;
			playSound(fall);
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player0"))
		{
			rigid0.constraints = originalCons;
			rigid0.GetComponent<Collider>().isTrigger = false;
			captured0 = false;
		}
		if (col.CompareTag("Player1"))
		{
			rigid1.constraints = originalCons;
			rigid1.GetComponent<Collider>().isTrigger = false;
			captured1 = false;
		}
	}
	
	void playSound(AudioClip sfx)
	{
		float vol = Random.Range(minVol, maxVol);
		GetComponent<AudioSource>().pitch = Random.Range (minPitch, maxPitch);
		GetComponent<AudioSource>().PlayOneShot(sfx, vol);
	}
}
