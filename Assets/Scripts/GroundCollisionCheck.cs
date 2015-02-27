using UnityEngine;
using System.Collections;

public class GroundCollisionCheck : MonoBehaviour {

	public GameObject character;
	public CharacterMotion cm;

	// Use this for initialization
	void Start () {
		character = transform.parent.gameObject;
		cm = character.GetComponent<CharacterMotion>();
	}
	
	void OnTriggerStay2D(Collider2D others){
		cm.isGrounded = true;
	}

	void OnTriggerExit2D(){
		cm.isGrounded = false;
	}

}
