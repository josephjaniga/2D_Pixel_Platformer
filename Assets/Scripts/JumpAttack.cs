using UnityEngine;
using System.Collections;

public class JumpAttack : MonoBehaviour {

	public GameObject player;
	public CharacterMotion playerMotion;

	public int JumpAttackDamageValue = 1;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		if ( player == null ){
			player = gameObject.transform.parent.gameObject;
		}
		playerMotion = player.GetComponent<CharacterMotion>();
	}

	void OnTriggerEnter2D (Collider2D col){
		if ( gameObject.name == "JumpAttackBumper" && col.gameObject.tag == "Mob" && !playerMotion.isGrounded ){
			col.gameObject.SendMessage("TakeDamage", JumpAttackDamageValue, SendMessageOptions.DontRequireReceiver);
			player.SendMessage("Jump", false, SendMessageOptions.DontRequireReceiver);
		}
	}

}
