using UnityEngine;
using System.Collections;

public class DoorTraverser : MonoBehaviour {

	public Door doorInformation;

	void OnTriggerEnter2D (Collider2D col){
		if ( col.gameObject.tag == "Player" ){
			col.gameObject.GetComponent<CharacterMotion>().targetPosition = new Vector3(doorInformation.targetPositionX, doorInformation.targetPositionY, 0f);
			col.gameObject.GetComponent<CharacterMotion>().shouldUseStartingPosition = false;
			Application.LoadLevel(doorInformation.targetScene);
		}
	}

}
