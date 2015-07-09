using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinterControl : MonoBehaviour {

	public GameObject SummerTrees;
	public GameObject WinterTrees;


	GameObject[] rocks;

	public Material cliff;
	public Material cliff_Ice;
	
	bool isWinter = false;

	
	public Text text;
	public Color c;
	int state = 0;
	float delay = 0f;
	float maxDelay = 1f;
	// Use this for initialization
	void Awake () 
	{
		rocks = GameObject.FindGameObjectsWithTag("Rock");
		c = text.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		isWinter = IceMode.icemodeOn;

		//display Text
		if(SnowMachine.snowing && state == 0)
		{
			text.text = "Winter Is Coming!";
			text.color = c;
			state = 1;
			delay = 0;
		}

		if(SnowFlake.snowflakeCount >= IceMode.amountOfSnowflakesForForever/2 && state == 1)
		{
			text.text = "Winter Is Near!";
			text.color = c;
			state = 2;
			delay = 0;
		}
		/*if(isWinter && state == 2)
		{
			text.fontSize = 120;
			text.text = "Merry Christmas!";
			text.color = c;
			state = 3;
			delay = 0;
		}*/
		if(delay > maxDelay)
			text.color = Color.Lerp(text.color, Color.clear, 1.5f * Time.deltaTime);
		else
			delay += Time.deltaTime;

		if(isWinter )
		{
			foreach(GameObject g in rocks)
			{
				g.GetComponent<MeshRenderer>().material = cliff_Ice;
			}
		}
		else
		{
			foreach(GameObject g in rocks)
			{
				g.GetComponent<MeshRenderer>().material = cliff;
			}
			
		}
		SummerTrees.SetActive(!isWinter);
		WinterTrees.SetActive(isWinter);
	}
}
