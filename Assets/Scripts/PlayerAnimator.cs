using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	public Animator a;
	public CharacterMotion cm;
	
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator>();
		cm = GetComponent<CharacterMotion>();
	}
	
	// Update is called once per frame
	void Update () {
	
		// Jump ascending / descending
		if ( GetComponent<Rigidbody2D>().velocity.y > 0 ){
			a.SetBool("isCrouching", true);
		} else if ( GetComponent<Rigidbody2D>().velocity.y < 0 ) {
			a.SetBool("isCrouching", false);
			a.SetBool("isWalking", false);
		}

		if ( !cm.isGrounded && GetComponent<Rigidbody2D>().velocity.y < 0  ){
			a.SetBool("isFalling", true);
			a.SetBool("isCrouching", false);
			a.SetBool("isWalking", false);
		} else if ( !cm.isGrounded && GetComponent<Rigidbody2D>().velocity.y > 0  ) {
			a.SetBool("isFalling", false);
			a.SetBool("isCrouching", true);
			a.SetBool("isWalking", false);
		} else {
			a.SetBool("isFalling", false);
		}

		// walking
		if ( ( cm.isMovingLeft || cm.isMovingRight ) && GetComponent<Rigidbody2D>().velocity.y == 0  ){
			a.SetBool("isWalking", true);
		} else {
			a.SetBool("isWalking", false);
		}

	}
}
