using UnityEngine;
using System.Collections;

public class LionBossAnimator : MonoBehaviour {
	
	public Animator a;
	public CharacterMotion cm;
	
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator>();
		cm = GetComponent<CharacterMotion>();
	}
	
	// Update is called once per frame
	void Update () {
		
		// walking
		if ( !cm.isAttacking ){
			a.SetBool("isAttacking", false);
			if ( ( cm.isMovingLeft || cm.isMovingRight ) && GetComponent<Rigidbody2D>().velocity.y == 0  ){
				a.SetBool("isWalking", true);
			} else {
				a.SetBool("isWalking", false);
			}
		} else {// attacking
			a.SetBool("isWalking", false);
			a.SetBool("isAttacking", true);
		}

	}
}
