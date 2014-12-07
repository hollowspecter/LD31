using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour {

	public float fallspeed = 80.0f;

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
			rigid0.collider.isTrigger = true;
		}
		if (captured1)
		{
			rigid1.velocity = new Vector3(0,-1 * fallspeed,0);
			
			originalCons = rigid1.constraints;
			rigid1.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			rigid1.collider.isTrigger = true;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0"))
		{
			playerT0 = col.transform;
			rigid0 = playerT0.rigidbody;

			captured0 = true;
		}
		if (col.CompareTag("Player1"))
		{
			playerT1 = col.transform;
			rigid1 = playerT1.rigidbody;

			captured1 = true;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player0"))
		{
			rigid0.constraints = originalCons;
			rigid0.collider.isTrigger = false;
			captured0 = false;
		}
		if (col.CompareTag("Player1"))
		{
			rigid1.constraints = originalCons;
			rigid1.collider.isTrigger = false;
			captured1 = false;
		}
	}
}
