using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;
	
	public AudioClip snowballHit;
	
	GameObject iceblock0;
	GameObject iceblock1;

	int player;
	
	void Awake()
	{
		if (CompareTag("Player0bullet"))
			player = 0;
		else
			player = 1;
	}

	void OnTriggerEnter(Collider col)
	{
		if (player==0)
		{
			if (col.CompareTag("Player0"))
			{
				Debug.Log ("Snowball Hit player0!");
				HitPlayer(col);
			}
		}else if (player==1)
		{
			if (col.CompareTag("Player1"))
			{
				Debug.Log ("Snowball Hit player1!");
				HitPlayer(col);
			}
		}
		else if (col.CompareTag("Obstacle"))
		{
			Destroy(gameObject);
		}
	}
	
	void HitPlayer(Collider col)
	{
		col.transform.FindChild("iceblock").GetComponent<iecblock>().Activate();
		playSound(snowballHit);
		Destroy(gameObject);
	}
	
	void playSound(AudioClip sfx)
	{
		float vol = Random.Range(minVol, maxVol);
		audio.pitch = Random.Range (minPitch, maxPitch);
		audio.PlayOneShot(sfx, vol);
	}
}
