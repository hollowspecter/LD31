using UnityEngine;
using System.Collections;

public class SnowFlake : MonoBehaviour {

	Renderer render;
	
	void Awake()
	{
		render = GetComponentInChildren<Renderer>();
	}

	void Update () {
		if (!render.isVisible)
			Destroy(gameObject);
	}
}
