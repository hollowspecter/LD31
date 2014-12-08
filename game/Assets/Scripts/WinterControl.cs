using UnityEngine;
using System.Collections;

public class WinterControl : MonoBehaviour {

	public GameObject SummerTrees;
	public GameObject WinterTrees;

	GameObject floor;
	GameObject[] rocks;

	public Material cliff;
	public Material cliff_Ice;
	
	bool isWinter = false;

	// Use this for initialization
	void Awake () 
	{
		rocks = GameObject.FindGameObjectsWithTag("Rock");
		floor = GameObject.FindGameObjectWithTag("Floor");
	}
	
	// Update is called once per frame
	void Update () 
	{
		isWinter = IceMode.icemodeOn;

		if(isWinter)
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
