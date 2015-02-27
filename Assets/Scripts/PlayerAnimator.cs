using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	public Animator a;
	public CharacterMotion cm;
	public Transform player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
		a = GetComponent<Animator>();
		cm = GetComponent<CharacterMotion>();
	}
	
	// Update is called once per frame
	void Update () {
	
		// face left / right
		if ( cm.isMovingLeft )	{ player.localScale = new Vector3(-1, 1, 1); }
		if ( cm.isMovingRight )	{ player.localScale = new Vector3( 1, 1, 1); }

		// Jump ascending / descending
		if ( rigidbody2D.velocity.y > 0 ){
			a.SetBool("isCrouching", true);
		} else if ( rigidbody2D.velocity.y < 0 ) {
			a.SetBool("isCrouching", false);
			a.SetBool("isWalking", false);
		}

		if ( !cm.isGrounded && rigidbody2D.velocity.y < 0  ){
			a.SetBool("isFalling", true);
			a.SetBool("isCrouching", false);
			a.SetBool("isWalking", false);
		} else if ( !cm.isGrounded && rigidbody2D.velocity.y > 0  ) {
			a.SetBool("isFalling", false);
			a.SetBool("isCrouching", true);
			a.SetBool("isWalking", false);
		} else {
			a.SetBool("isFalling", false);
		}

		// walking
		if ( ( cm.isMovingLeft || cm.isMovingRight ) && rigidbody2D.velocity.y == 0  ){
			a.SetBool("isWalking", true);
		} else {
			a.SetBool("isWalking", false);
		}

	}
}
