using UnityEngine;
using System.Collections;

public class Obtainable : MonoBehaviour {
	
	void OnCollisionEnter2D (Collision2D col){
		if ( col.gameObject.tag == "Player" ){
			col.gameObject.SendMessage("addItem", gameObject.name, SendMessageOptions.DontRequireReceiver);	
			PickedUp();
			col.gameObject.SendMessage("ClearBlockers", SendMessageOptions.DontRequireReceiver);

			// send a chat message
			_.chatManager.GetComponent<ChatManager>().CreateChatMessage(
					col.gameObject.transform.position + new Vector3(0f, 2.25f, 0f),
					"Picked Up the " + gameObject.name + "!"
				);
		}
	}

	public void PickedUp(){
		// TODO: play picked up noise

		Destroy(gameObject);

	}

}
