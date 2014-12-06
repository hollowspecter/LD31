using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	private Transform myTransform;
	private Transform mainCam;
	private bool flipped;
	
	void Awake ()
	{
		myTransform = transform; //cache the transform
		mainCam = Camera.main.transform; //cache the transform of the camera
	}
	
	void LateUpdate ()
	{
		myTransform.LookAt(mainCam);
	}
}
