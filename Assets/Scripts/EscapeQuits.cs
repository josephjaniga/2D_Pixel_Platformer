using UnityEngine;
using System.Collections;

public class EscapeQuits : MonoBehaviour {

	void Start()
	{
		Destroy(GameObject.Find ("Canvas_Overlay"));
		Destroy(GameObject.Find ("Player"));
	}

	void Update () {
		if ( Input.GetKeyDown(KeyCode.Escape) ){
			Application.Quit();
		}
	}

}
