using UnityEngine;
using System.Collections;

public class iecblock : MonoBehaviour {

	public float duration = 5.0f;
	
	bool active = false;
	
	PlayerIcemode player;
	Movement playermov;
	float timer;
	MeshRenderer render;
	
	void Awake()
	{
		render = GetComponent<MeshRenderer>();
		player = transform.parent.GetComponent<PlayerIcemode>();
		playermov = transform.parent.GetComponent<PlayerMovement>();
		if(playermov == null)
			playermov = transform.parent.GetComponent<AI_Movement>();
		Deactivate();
	}
	
	void Update()
	{
		if (active)
		{
			timer += Time.deltaTime;
			if (timer > duration)
				Deactivate();
		}
	}
	
	public void Activate()
	{
		render.enabled = true;
		player.iceblock = true;
		playermov.blockMovement = true;
		timer = 0.0f;
		active = true;
	}
	
	public void Deactivate()
	{
		render.enabled = false;
		playermov.blockMovement = false;
		player.iceblock = false;
		active = false;
	}
}
