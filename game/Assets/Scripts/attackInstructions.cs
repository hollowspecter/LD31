using UnityEngine;
using System.Collections;

public class attackInstructions : MonoBehaviour {

	public GameObject instructions;
	
	Animator anim;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetButtonDown("Hit_0") || Input.GetButtonDown("Hit_1"))
		{
			anim.SetTrigger("disappear");
		}
	}
	
	public void deactivate()
	{
		instructions.SetActive(true);
		gameObject.SetActive(false);
	}
}
