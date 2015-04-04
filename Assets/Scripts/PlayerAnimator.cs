using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	// cloak animator
	public Animator cloak_a;

	// adventurer animator
	public Animator adventurer_a;

	// boots animators
	public Animator boots_a;
	
	// dagger animators
	public Animator dagger_a;

	public CharacterMotion cm;

	public float LastYVelocity = 0f;
	
	// Use this for initialization
	void Start () {
		cloak_a = GetComponent<Animator>();
		dagger_a = gameObject.transform.Find("Dagger").GetComponent<Animator>();
		adventurer_a = gameObject.transform.Find("Adventurer").GetComponent<Animator>();
		boots_a = gameObject.transform.Find("Boots").GetComponent<Animator>();
		cm = GetComponent<CharacterMotion>();
	}
	
	// Update is called once per frame
	void Update () {

		if ( cm.isAttacking && _.playerInventory.canAttack ){

			setAllAnimators("isAttacking", true);
			setAllAnimators("isFalling", 		false);
			setAllAnimators("isAscending", 	false);
			setAllAnimators("isWalking", 		false);

		} else {

			setAllAnimators("isAttacking", false);

			// Jump ascending / descending
			if ( GetComponent<Rigidbody2D>().velocity.y > 0 ){
				setAllAnimators("isAscending", true);
			} else if ( GetComponent<Rigidbody2D>().velocity.y < 0 ) {
				setAllAnimators("isFalling", true);
				setAllAnimators("isAscending", false);
				setAllAnimators("isCrouching", false);
				setAllAnimators("isWalking", false);
			}
			
			if ( !cm.isGrounded && GetComponent<Rigidbody2D>().velocity.y < 0  ){
				setAllAnimators("isFalling", true);
				setAllAnimators("isCrouching", false);
				setAllAnimators("isWalking", false);
			} else if ( !cm.isGrounded && GetComponent<Rigidbody2D>().velocity.y > 0  ) {
				setAllAnimators("isFalling", false);
				setAllAnimators("isAscending", true);
				setAllAnimators("isWalking", false);
			} else {
				// Landed
				setAllAnimators("isFalling", false);
				if ( LastYVelocity < 0f ){
					_.player.SendMessage("PlayBumpClip", SendMessageOptions.DontRequireReceiver);
				}
			}
			
			// walking
			if ( ( cm.isMovingLeft || cm.isMovingRight ) && GetComponent<Rigidbody2D>().velocity.y == 0  ){
				setAllAnimators("isWalking", true);
			} else {
				setAllAnimators("isWalking", false);
			}

		}

		LastYVelocity = GetComponent<Rigidbody2D>().velocity.y;

	}

	public void setAllAnimators(string Parameter, bool Value){

		if ( Parameter == "isKilled" ){
			cloak_a.SetTrigger(Parameter);
		} else {
			// the player animator
			cloak_a.SetBool(Parameter, Value);
			// the player animator
			adventurer_a.SetBool(Parameter, Value);
			// the boots animator
			boots_a.SetBool(Parameter, Value);
			// the dagger animator
			dagger_a.SetBool(Parameter, Value);
		}

	}


}
