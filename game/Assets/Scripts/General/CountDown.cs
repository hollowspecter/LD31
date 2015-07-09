using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour {

	public float countdown = 5.0f;
	public float fadeAwaySpeed = 1.0f;
	public Movement pm1;
	public Movement pm2;
	public int gosize = 100;
	
	public AudioSource wintermusic;
	
	public AudioClip music;



	bool activated = false;
	
	Text text;	
	void Awake()
	{
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		if (countdown > 0)
		{
			pm1.blockMovement = true;
			pm2.blockMovement = true;
			text.text = "GET READY\n"+Mathf.Round(countdown);
			countdown -= Time.deltaTime;
		}
		else
		{
			if (!activated)
			{
				pm1.blockMovement = false;
				pm2.blockMovement = false;
			}
			
			text.fontSize = gosize;
			text.text = "GO!!";
			
			text.color = Color.Lerp(text.color, Color.clear, fadeAwaySpeed * Time.deltaTime);
			
			if (!activated)
			{
				GetComponent<AudioSource>().clip = music;
				GetComponent<AudioSource>().Play();
				wintermusic.Play();
				activated = true;
			}
		}
	}
}
