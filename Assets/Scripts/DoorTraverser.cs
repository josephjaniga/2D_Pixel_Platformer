using UnityEngine;
using System.Collections;

public class DoorTraverser : MonoBehaviour {

	public Door doorInformation;

	public float scale = 1f;

	public bool isLocked = false;
	public string itemRequired = "";

	void Start(){
		isLocked = doorInformation.isLocked;
		itemRequired = doorInformation.itemRequired;
		scale = _.levelMapSystem.GetComponent<LevelMapSystem>().FG_map.scale;
	}

	void OnCollisionEnter2D (Collision2D col){
		if ( col.gameObject.tag == "Player" ){

			if ( !isLocked ){
				PlayerPassThroughDoor(col.gameObject);
			} else {
				// check if player has the required item
				if ( _.player.GetComponent<PlayerInventory>().hasItem(itemRequired) ) {
					_.player.GetComponent<PlayerInventory>().removeItem(itemRequired);
					isLocked = false;
					PlayerPassThroughDoor(col.gameObject);
				} else {
					_.chatManager.GetComponent<ChatManager>().CreateChatMessage(
							col.gameObject.transform.position + new Vector3(0f, 2.25f, 0f),
							"It's Locked!"
						);
				}
			}

		}
	}

	void PlayerPassThroughDoor(GameObject go){
		go.GetComponent<CharacterMotion>().targetPosition = new Vector3(doorInformation.targetPositionX, doorInformation.targetPositionY, 0f) * scale;
		go.GetComponent<CharacterMotion>().shouldUseStartingPosition = false;
		Application.LoadLevel(doorInformation.targetScene);
	}
	
}
