using UnityEngine;
using System.Collections;

public class PressStart : MonoBehaviour {

	public int levelToLoad = 0;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit"))
			Application.LoadLevel(levelToLoad);
	}
}
