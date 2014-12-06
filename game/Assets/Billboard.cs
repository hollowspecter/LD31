using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	private Transform myTransform;
	private Transform mainCam;
	
	void Awake ()
	{
		myTransform = transform; //cache the transform
		mainCam = Camera.main.transform; //cache the transform of the camera
	}
	
	void LateUpdate ()
	{
		myTransform.LookAt(myTransform.position + mainCam.position);
	}
}
