using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour {

	RigidbodyConstraints originalCons;
	Transform playerT0;
	Transform playerT1;
	Rigidbody rigid0;
	Rigidbody rigid1;
	PlayerMovement move0;
	PlayerMovement move1;
	bool captured0 = false;
	bool captured1 = false;

	void Update()
	{
		if (captured0)
		{
			rigid0.angularVelocity = Vector3.zero;
			playerT0.position = new Vector3(transform.position.x, playerT0.position.y, transform.position.z);
			
			originalCons = rigid0.constraints;
			rigid0.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			rigid0.collider.isTrigger = true;
		}
		if (captured1)
		{
			rigid1.angularVelocity = Vector3.zero;
			playerT1.position = new Vector3(transform.position.x, playerT1.position.y, transform.position.z);
			
			originalCons = rigid1.constraints;
			rigid1.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			rigid1.collider.isTrigger = true;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player0"))
		{
			//Debug.Log ("Captured Player0");
		
			playerT0 = col.transform;
			rigid0 = playerT0.rigidbody;
			move0 = playerT0.GetComponent<PlayerMovement>();
			
			playerT0.position = transform.position;
			
			captured0 = true;
		}
		if (col.CompareTag("Player1"))
		{
			//Debug.Log ("Captured Player1");
		
			playerT1 = col.transform;
			rigid1 = playerT1.rigidbody;
			move1 = playerT1.GetComponent<PlayerMovement>();
			
			playerT1.position = transform.position;
			
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
