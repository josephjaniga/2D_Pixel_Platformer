using UnityEngine;
using System.Collections;

public class RightCollisionCheck : MonoBehaviour {

	public GameObject character;
	public CharacterMotion cm;

	// Use this for initialization
	void Start () {
		character = transform.parent.gameObject;
		cm = character.GetComponent<CharacterMotion>();
	}
	
	void OnTriggerStay2D(Collider2D col){
		cm.rightLocked = true;
	}

	void OnTriggerExit2D(){
		cm.rightLocked = false;
	}

}
