using UnityEngine;
using System.Collections;

public class SnowGenerator : MonoBehaviour {

	public GameObject snowDecal;

	//how far can it snow around the snowgenerators position
	public float width;
	public float height;

	//the standard orientation of the decal
	public Transform standardOrientation;

	//private ArrayList sprites;
	private float timer;

	// Use this for initialization
	void Start () 
	{
		//sprites = ArrayList<sprites>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			resetTimer ();
			spawnSnowflake();
		}	
	}

	Vector3 calculatePosition()
	{
		Vector3 pos = transform.position;
		Vector2 rnd = Random.insideUnitCircle;
		pos.x += rnd.x * width;
		pos.y = 0.001f;
		pos.z += rnd.y * height;

		return pos;
	}

	void spawnSnowflake()
	{
		Vector3 position = calculatePosition();
		GameObject obj = (GameObject)Instantiate(snowDecal, position, Quaternion.identity);
		obj.transform.SetParent(this.transform);
		float r = Random.Range (0.4f, 1.6f);
		obj.transform.localScale = new Vector3(1.0f*r, 1.0f, 1.0f *r);

		r = Random.Range (0.4f, 1.0f);
		SpriteRenderer s;
		s = obj.GetComponentInChildren<SpriteRenderer>();
		s.color = new Color(1.0f, 1.0f, 1.0f, r); 
	}

	void resetTimer()
	{
		timer = Random.Range(0.25f, 0.5f);
	}
}
