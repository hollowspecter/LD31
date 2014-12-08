using UnityEngine;
using System.Collections;

public class SnowMachine : MonoBehaviour {

	//how many seconds until Winter/ Icemode
	public float timeTilSuddenDeath = 30.0f;
	public bool suddendeath = false;

	//Snowflakeprefab and fallheight
	public GameObject snowflake;
	public float snowflakeHeight = 30.0f;
	
	public static bool snowing = false;

	//the snowing area
	public float maxX = 28.0f;
	public float maxZ = 28.0f;

	//how much time between the snowflakes
	public float mindelay = 0.8f;
	public float maxdelay = 2.0f;

	float timer = 0.0f;

	bool isWinter;
	int snowFlakeNum = 1;
	int maxSnowFlakes = 10;

	void Update()
	{
		Debug.Log("test");
		//calculate number of snowflakes to spawn
		isWinter = IceMode.icemodeOn;
		snowFlakeNum = (int) (((float)SnowFlake.snowflakeCount/IceMode.amountOfSnowflakesForForever)*maxSnowFlakes);
		snowFlakeNum = snowFlakeNum > 0 ? snowFlakeNum : 1;
		snowFlakeNum = Mathf.Clamp(snowFlakeNum, 1, maxSnowFlakes);

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
			//Debug.Log("snowFlakeNum: " + snowFlakeNum);
			if(isWinter)
			{
				for(int i = 0; i < maxSnowFlakes; i++)
				{
					//Debug.Log("Snowflake");
					Vector2 point = Random.insideUnitCircle;
					GameObject obj = (GameObject)Instantiate (snowflake, new Vector3(point.x * maxX, snowflakeHeight, point.y * maxZ), Quaternion.identity);
					obj.transform.SetParent(this.transform);
					float scale = Random.Range(0.4f, 1.6f);
					obj.transform.localScale = new Vector3(scale, 1f, scale);
				}
				float delay = Random.Range(mindelay, maxdelay);
				yield return new WaitForSeconds(delay);
			}
			else
			{
				for(int i = 0; i < snowFlakeNum; i++)
				{
					Vector2 point = Random.insideUnitCircle;
					GameObject obj = (GameObject)Instantiate (snowflake, new Vector3(point.x * maxX, snowflakeHeight, point.y * maxZ), Quaternion.identity);
					obj.transform.SetParent(this.transform);
					float scale = Random.Range(0.4f, 1.6f);
					obj.transform.localScale = new Vector3(scale, 1f, scale);
				}
				float delay = Random.Range(mindelay, maxdelay);
				yield return new WaitForSeconds(delay);
			}

		}
	}
}
