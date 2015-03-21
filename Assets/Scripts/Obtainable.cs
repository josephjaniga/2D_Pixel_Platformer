using UnityEngine;
using System.Collections;

public class Obtainable : MonoBehaviour {
	
	void OnCollisionEnter2D (Collision2D col){
		if ( col.gameObject.tag == "Player" ){
			_.player.SendMessage("addItem", gameObject.name, SendMessageOptions.DontRequireReceiver);	
			PickedUp();

			// TODO: play picked up noise
			_.player.SendMessage("PlayItemPickUpClip", SendMessageOptions.DontRequireReceiver);

			// send a chat message
			_.chatManager.GetComponent<ChatManager>().CreateChatMessage(
					col.gameObject.transform.position + new Vector3(0f, 2.25f, 0f),
					"Picked Up the " + gameObject.name + "!"
				);

			_.player.SendMessage("ClearBlockers", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void PickedUp(){

		Destroy(gameObject);
	}

}
