using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	public Transform[] spawnPositions;
	public GameObject[] items;
	public float minTimer = 5.0f;
	public float maxTimer = 15.0f;
	
	float timer = 0.0f;
	
	void Awake()
	{
		resetTimer();
	}
	
	void Update()
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			resetTimer ();
			spawnItem();
		}
	}
	
	void resetTimer()
	{
		timer = Random.Range(minTimer, maxTimer);
	}
	
	void spawnItem()
	{
		int itemIndex = Random.Range (0, items.Length);
		int spawnIndex = Random.Range (0, spawnPositions.Length);
		
		Instantiate (items[itemIndex], spawnPositions[spawnIndex].position, Quaternion.identity);
	}
}
