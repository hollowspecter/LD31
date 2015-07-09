using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttons : MonoBehaviour {

	public Sprite controls;
	public Sprite attacks;
	public GameObject attacksObj;
	public GameObject controlsObj;
	
	int selected = 0;
	
	private bool m_isAxisInUse = false;

	// Update is called once per frame
	void Update () {
		float h0 = Input.GetAxisRaw ("Horizontal_0");
		float h1 = Input.GetAxisRaw ("Horizontal_1");
		
		if (h0 != 0 || h1 != 0)
		 {
		 	if (!m_isAxisInUse)
		 	{
				selected++;
				if (selected > 1)
					selected -= 2;
				
				switchImage ();
				m_isAxisInUse = true;
		 	}
		 }
		if (h0 == 0 && h1 == 0)
		{
			m_isAxisInUse = false;
		}
		 
		 
		 if (Input.GetButtonDown("Hit_0") || Input.GetButtonDown("Hit_1"))
		 {
			if (selected == 1)
			{
				controlsObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else
			{
				attacksObj.SetActive(true);
				gameObject.SetActive(false);
			}
		 }
	}
	
	void switchImage()
	{
		if (selected == 1)
			GetComponent<Image>().sprite = controls;
		else
			GetComponent<Image>().sprite = attacks;
	}
}
