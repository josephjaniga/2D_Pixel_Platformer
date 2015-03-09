using UnityEngine;
using System.Collections;

public class ChatManager : MonoBehaviour {

	public GameObject worldCanvas;

	void Start(){
		worldCanvas = _.canvasWorld;
	}
		
	public void CreateChatMessage(Vector3 position, string msg){

		Vector3 messagePosition = new Vector3(position.x, position.y, -2f);

		GameObject temp = GameObject.Instantiate(
				Resources.Load("Prefabs/UI/ChatBubble"), 
				messagePosition,
				Quaternion.identity
			) as GameObject;

		temp.GetComponent<ChatBubble>().background.text = msg;
		temp.GetComponent<ChatBubble>().foreground.text = msg;

		temp.transform.parent = worldCanvas.transform;

	}


}
