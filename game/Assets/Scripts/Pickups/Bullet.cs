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

	void OnTriggerEnter(Collider col)
	{
		if (player==0)
		{
			if (col.CompareTag("Player1"))
			{
				Debug.Log ("Snowball Hit player1!");
				HitPlayer(col);
			}
		}else if (player==1)
		{
			if (col.CompareTag("Player0"))
			{
				Debug.Log ("Snowball Hit player0!");
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
		GetComponent<AudioSource>().pitch = Random.Range (minPitch, maxPitch);
		GetComponent<AudioSource>().PlayOneShot(sfx, vol);
	}
	
	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}
	
	public void setPlayer(int p)
	{
		player = p;
	}
}
