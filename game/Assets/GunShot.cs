using UnityEngine;
using System.Collections;

public class GunShot : MonoBehaviour {

	public GameObject bullet;
	public float speed = 5.0f;
	
	Transform gunpoint;
	Transform myTrans;
	
	void Awake()
	{
		myTrans = transform;
		gunpoint = transform.FindChild("GunPoint");
	}
	
	
	public void shootRight()
	{
		Vector3 direction = gunpoint.position - myTrans.position;
		direction.Normalize();
		GameObject bulletInstance = (GameObject) Instantiate (bullet, gunpoint.position, Quaternion.Euler(new Vector3(0,0,0)));
		bulletInstance.tag = tag+"bullet";
		bulletInstance.GetComponent<Rigidbody>().velocity = direction * speed;
	}
	
	public void shootLeft()
	{
		Vector3 direction = gunpoint.position - myTrans.position;
		direction.Normalize();
		GameObject bulletInstance = (GameObject) Instantiate (bullet, gunpoint.position, Quaternion.Euler(new Vector3(0,0,0)));
		bulletInstance.tag = tag+"bullet";
		bulletInstance.GetComponent<Rigidbody>().velocity = direction * speed;
	}
}
