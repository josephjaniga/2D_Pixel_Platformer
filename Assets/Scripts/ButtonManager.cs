using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public bool pressed = false;

	public Button b;

	void Start(){
		b = gameObject.GetComponent<Button>();
	}

	public void PressedDown(){
		pressed = true;
	}

	public void PressedUp(){
		pressed = false;
	}

}
