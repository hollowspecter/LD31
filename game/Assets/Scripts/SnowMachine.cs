using UnityEngine;
using System.Collections;

public class SnowMachine : MonoBehaviour {

	public float timeTilSuddenDeath = 30.0f;
	public bool suddendeath = false;

	public GameObject snowflake;
	public float snowflakeHeight = 30.0f;

	public float maxX = 28.0f;
	public float maxZ = 28.0f;

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
			Vector2 point = Random.insideUnitCircle;
			GameObject obj = (GameObject)Instantiate (snowflake, new Vector3(point.x * maxX, snowflakeHeight, point.y * maxZ), Quaternion.identity);
			obj.transform.SetParent(this.transform);
			float scale = Random.Range(0.4f, 1.6f);
			obj.transform.localScale = new Vector3(scale, 1f, scale);
			float delay = Random.Range(mindelay, maxdelay);
			yield return new WaitForSeconds(delay);
		}
	}
}
