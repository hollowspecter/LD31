using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartsView : MonoBehaviour {

	Image[] hearts = new Image[5];

	void Awake()
	{
		for (int i=0; i<5; i++)
		{
			hearts[i] = transform.GetChild(i).GetComponent<Image>();
		}
	}
	
	public void reduceHeart(int lifes)
	{
		if (lifes >= 0)
			hearts[lifes].color = Color.clear;
	}
}
