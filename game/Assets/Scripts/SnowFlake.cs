using UnityEngine;
using System.Collections;

public class SnowFlake : MonoBehaviour 
{
	public static int snowflakeCount = 0;
	
	public GameObject snowDecal;

	Renderer render;

	void Awake()
	{
		render = GetComponentInChildren<Renderer>();
	}

	void Update () 
	{
		if (transform.position.y < 0)
			Destroy(gameObject);
	}

	void OnCollisionEnter(Collision col)
	{
		//Debug.Log("snowflake hit:" + col.collider.tag);
		if(col.collider.tag == "Floor")
		{
			spawnSnowDecal();
			snowflakeCount++;
		}
	}

	void spawnSnowDecal()
	{
		Vector3 pos = new Vector3(this.transform.position.x, 0.001f, this.transform.position.z);
		//Debug.Log("SpawnSnowDecal at:" + pos);
		GameObject obj = (GameObject) Instantiate(snowDecal, pos, Quaternion.identity);
		obj.transform.SetParent(this.transform.parent);
		float scale = Random.Range(0.4f, 2f);
		obj.transform.localScale = new Vector3(scale, 1f, scale);
		SpriteRenderer s = obj.GetComponentInChildren<SpriteRenderer>();
		s.color = new Color (1f, 1f, 1f, Random.Range(0.4f, 1f));

		Destroy(this.gameObject);
	}

}
