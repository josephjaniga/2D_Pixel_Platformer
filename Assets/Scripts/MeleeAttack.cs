using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

	public GameObject player;
	public CharacterMotion playerMotion;

	public Animator weaponAnimator;
	public BoxCollider2D weaponCollider;

	public int MeleeAttackDamageValue = 1;

	public float lastAttack = 0f;
	public float meleeAttackSpeed = 0.75f;


	public bool isAttacking = false;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player");
		if ( player == null ){
			player = gameObject.transform.parent.parent.gameObject;
		}
		playerMotion = player.GetComponent<CharacterMotion>();

		weaponAnimator = transform.parent.gameObject.GetComponent<Animator>();
		weaponCollider = gameObject.GetComponent<BoxCollider2D>();
	}

	void Update() {

		if ( Input.GetKey(KeyCode.LeftShift) && !isAttacking ){
			lastAttack = Time.time;
			isAttacking = true;
			weaponAnimator.Play("MeleeWeapon_Attack");
			playerMotion.rightLocked = false;
			playerMotion.leftLocked = false;
		}

		if ( Time.time >= lastAttack + meleeAttackSpeed ){
			isAttacking = false;
		}

		if (isAttacking){
			weaponCollider.enabled = true;
		} else {
			weaponCollider.enabled = false;
		}

	}

	void OnTriggerEnter2D (Collider2D col){
		if ( gameObject.name == "MeleeAttackBumper" && col.gameObject.tag == "Mob" ){
			col.gameObject.SendMessage("TakeDamage", MeleeAttackDamageValue, SendMessageOptions.DontRequireReceiver);
		}
	}

}
