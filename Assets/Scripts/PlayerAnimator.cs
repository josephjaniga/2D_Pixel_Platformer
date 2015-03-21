using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	public Animator a;
	public CharacterMotion cm;

	public float LastYVelocity = 0f;
	
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator>();
		cm = GetComponent<CharacterMotion>();
	}
	
	// Update is called once per frame
	void Update () {

		if ( cm.isAttacking ){
			a.SetBool("isAttacking", true);

			a.SetBool("isFalling", 		false);
			a.SetBool("isAscending", 	false);
			a.SetBool("isWalking", 		false);
		} else {

			a.SetBool("isAttacking", false);

			// Jump ascending / descending
			if ( GetComponent<Rigidbody2D>().velocity.y > 0 ){
				a.SetBool("isAscending", true);
			} else if ( GetComponent<Rigidbody2D>().velocity.y < 0 ) {
				a.SetBool("isFalling", true);
				a.SetBool("isAscending", false);
				a.SetBool("isCrouching", false);
				a.SetBool("isWalking", false);
			}
			
			if ( !cm.isGrounded && GetComponent<Rigidbody2D>().velocity.y < 0  ){
				a.SetBool("isFalling", true);
				a.SetBool("isCrouching", false);
				a.SetBool("isWalking", false);
			} else if ( !cm.isGrounded && GetComponent<Rigidbody2D>().velocity.y > 0  ) {
				a.SetBool("isFalling", false);
				a.SetBool("isAscending", true);
				a.SetBool("isWalking", false);
			} else {
				// Landed
				a.SetBool("isFalling", false);
				if ( LastYVelocity < 0f ){
					_.player.SendMessage("PlayBumpClip", SendMessageOptions.DontRequireReceiver);
				}
			}
			
			// walking
			if ( ( cm.isMovingLeft || cm.isMovingRight ) && GetComponent<Rigidbody2D>().velocity.y == 0  ){
				a.SetBool("isWalking", true);
			} else {
				a.SetBool("isWalking", false);
			}

		}

		LastYVelocity = GetComponent<Rigidbody2D>().velocity.y;


	}
}
