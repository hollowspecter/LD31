using UnityEngine;
using System.Collections;

public class WinterMusic : MonoBehaviour {

	public AudioSource oldmusic;
	public float crossfadeDuration = 3.0f;
	public float maxVol = 0.66f;
	

	bool isWinter = false;
	bool lerpStarted = false;
	bool crossfade = false;
	
	float timeFadeStarted;

	void Update()
	{
		isWinter = IceMode.icemodeOn;
		
		if (isWinter && !lerpStarted)
		{
			crossfade = true;
			timeFadeStarted = Time.time;
		}
		
		if (crossfade)
		{
			lerpStarted = true;
			float timeGone = Time.time - timeFadeStarted;
			float percantageComplete = timeGone / crossfadeDuration;
			float newVol = Mathf.Lerp(0, maxVol, percantageComplete);

			audio.volume = newVol;
			oldmusic.volume = maxVol - newVol;
			
		
		}
	}
}
