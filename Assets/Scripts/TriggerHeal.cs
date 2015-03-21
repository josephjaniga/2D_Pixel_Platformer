using UnityEngine;
using System.Collections;

public class TriggerHeal : MonoBehaviour {

	public GameObject player;
	public CharacterHealth playerHealth;
	
	public int healValue = 1;

	public bool consumedOnTrigger = true;
	
	// Use this for initialization
	void Start () {
		player = _.player;
		playerHealth = player.GetComponent<CharacterHealth>();
	}
	
	void OnTriggerEnter2D (Collider2D col){
		if ( col.gameObject.tag == "Player" ){
			_.player.SendMessage("PlayItemPickUpClip", SendMessageOptions.DontRequireReceiver);
			col.gameObject.SendMessage("Heal", healValue, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}


}
