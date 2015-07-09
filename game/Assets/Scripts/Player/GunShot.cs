using UnityEngine;
using System.Collections;

public class GunShot : MonoBehaviour {

	public int maxAmmo = 3;
	public int player;
	
	int ammo = 3;

	public GameObject bullet;
	public float speed = 5.0f;
	
	Transform gunpoint;
	Transform myTrans;
	Animator anim;
	GameObject snowcanon;
	
	void Awake()
	{
		myTrans = transform;
		gunpoint = transform.FindChild("GunPoint");
		anim = GetComponent<Animator>();
		snowcanon = transform.FindChild("Sprite").FindChild("snowcanon").gameObject;
	}
	
	void Update()
	{
		bool weapon = anim.GetBool("Gun");
		if (weapon)
			snowcanon.SetActive(true);
		else
			snowcanon.SetActive(false);

		if( anim.GetBool("Right") )
		{
			snowcanon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.2f);
		}
		else
		{
			snowcanon.transform.localPosition = new Vector3(0.0f, 0.0f, -0.2f);
		}
			
	}
	
	public void shootRight()
	{
		Vector3 direction = gunpoint.position - myTrans.position;
		
		float newz = Mathf.Clamp(direction.z, -0.35f, 0.35f);
		direction = new Vector3(1f, direction.y, newz);
		
		direction.Normalize();
		
		GameObject bulletInstance = (GameObject) Instantiate (bullet, gunpoint.position, Quaternion.Euler(new Vector3(0,0,0)));
		bulletInstance.GetComponent<Bullet>().setPlayer(player);
		bulletInstance.GetComponent<Rigidbody>().velocity = direction * speed;


		Debug.Log("velo =" + bulletInstance.GetComponent<Rigidbody>().velocity);
		checkAmmo();
	}
	
	public void shootLeft()
	{
		Vector3 direction = gunpoint.position - myTrans.position;
		
		float newz = Mathf.Clamp(direction.z, -0.35f, 0.35f);
		direction = new Vector3(-1f, direction.y, newz);
		
		direction.Normalize();
		GameObject bulletInstance = (GameObject) Instantiate (bullet, gunpoint.position, Quaternion.Euler(new Vector3(0,0,0)));
		bulletInstance.GetComponent<Bullet>().setPlayer(player);
		bulletInstance.GetComponent<Rigidbody>().velocity = direction * speed;
		
		checkAmmo();
	}
	
	void checkAmmo()
	{
		ammo--;
		if (ammo <= 0)
		{
			anim.SetBool("Gun", false);
			ammo = maxAmmo;
			/*
			Make it appear in a different way maybe
			*/
		}
	}
}
