using UnityEngine;
using System.Collections;

public class SnowMachine : MonoBehaviour {

	public float timeTilSuddenDeath = 30.0f;
	public bool suddendeath = false;
	public GameObject snowflake;
	public float snowflakeHeight = 30.0f;
	public float maxX = 30.0f;
	public float maxZ = 30.0f;
	public float mindelay = 0.8f;
	public float maxdelay = 2.0f;
	
	float timer = 0.0f;
	bool snowing = false;
	
	void Update()
	{
		if (!suddendeath)
		{
			timer += Time.deltaTime;
			if (timer >= timeTilSuddenDeath)
			{
				suddendeath = true;
			}
		}
		else
		{
			if (!snowing)
				StartCoroutine("snow");
		}
	}
	
	IEnumerator snow()
	{
		snowing = true;
		while(snowing)
		{
			float x = Random.Range(-maxX, maxX);
			float z = Random.Range(-maxZ, maxZ);
		
			Instantiate (snowflake, new Vector3(x, snowflakeHeight, z), Quaternion.identity);
		
			float delay = Random.Range(mindelay, maxdelay);
			yield return new WaitForSeconds(delay);
		}
	}
}
