﻿using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Collections;
using Newtonsoft.Json;

public class CharacterMotion : MonoBehaviour
{

	public GameObject character;

	// Input Interface
	public ICharacterInputTypes type;
	public ICharacterInput characterInput;

	// running & jumping
	public float runSpeed = 105f;
	public float jumpForce= 550f;
	private float MAX_Y_VELOCITY = 300f;

	private Vector2 acceleration 	= new Vector2(0f, 0f);
	private Vector2 velocity 		= new Vector2(0f, 0f);

	// Motion Target Booleans
	public bool isMovingLeft 	= false;
	public bool isMovingRight 	= false;
	public bool isJumping 		= false;

	// attack target boolean?
	public bool isAttacking		= false;

	// Collision Indicators
	public bool isGrounded 		= false;
	public bool leftLocked		= false;
	public bool rightLocked		= false;

	public bool canKnockBack = true;
	public bool isKnockingBack = false;
	public bool shouldKnockBack = false;

	public bool shouldUseStartingPosition = true;
	public Vector3 targetPosition = Vector3.zero;

	// Use this for initialization
	void Start () {

		character = gameObject;

		switch(type){
			case ICharacterInputTypes.CharacterKeyboardInput:
				characterInput = new CharacterKeyboardInput(this);
			break;
			case ICharacterInputTypes.CharacterTouchInput:
				characterInput = new CharacterTouchInput(this);
			break;
			case ICharacterInputTypes.RandomAI:
				characterInput = new RandomAI(this);
			break;
			case ICharacterInputTypes.DumbJumpAI:
				characterInput = new DumbJumpAI(this);
			break;
			case ICharacterInputTypes.BlankAI:
				characterInput = new BlankAI(this);
			break;
			case ICharacterInputTypes.LionBossAI:
				characterInput = new LionBossAI(this);
			break;
		}

		characterInput.Start();

		InvokeRepeating("ClearBlockers", .5f, .25f);

	}

	// Update is called once per frame
	void FixedUpdate () {

		// check the Input Interface and Update the Motion Target Booleans
		characterInput.Update();

		acceleration = new Vector2(0f,0f);

		if ( isGrounded && isKnockingBack ){
			isKnockingBack = false;
		}
	
		// moving by foot
		// LEFT
		if ( isMovingLeft && !rightLocked ){ // !RIGHTLOCKED - adjust for the flipped collider on left
			acceleration.x -= runSpeed;
		} else if ( isMovingRight && !rightLocked ){ // right
			acceleration.x += runSpeed;
		}

		if ( isJumping && isGrounded ){
			Jump(true);
			isGrounded = false;
		}

		// physics fixes?
		velocity += acceleration * Time.deltaTime;		// set the velocity

		// Clamp Y Velocity every frame
		if ( GetComponent<Rigidbody2D>().velocity.y >= MAX_Y_VELOCITY )
			GetComponent<Rigidbody2D>().velocity = new Vector2( GetComponent<Rigidbody2D>().velocity.x, MAX_Y_VELOCITY );
		if ( GetComponent<Rigidbody2D>().velocity.y <= -MAX_Y_VELOCITY )
			GetComponent<Rigidbody2D>().velocity = new Vector2( GetComponent<Rigidbody2D>().velocity.x, -MAX_Y_VELOCITY );

		transform.Translate(velocity * Time.deltaTime);	// translate based on velocity
		velocity.x *= 0.8f;								// decay x velocity every frame

	}

	public void Jump(bool shouldJump = true){
		if ( shouldJump ){
			GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, jumpForce, 0f));
		} else {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
		}
	}


	public void Bounce(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y * 1.1f);
	}

	/**
	 * TODO: DOES THIS MAKE SENSE?
	 */
	public void TakeDamage(int DamageAmount){
		if ( !isKnockingBack && canKnockBack ){
			shouldKnockBack = true;
			KnockBack();
		}
	}
	
	public void KnockBack(){
		if ( shouldKnockBack ){
			if ( !isGrounded ){
				GetComponent<Rigidbody2D>().velocity = new Vector2( -transform.localScale.x * 5f, GetComponent<Rigidbody2D>().velocity.y/2f + 6f );
			} else {
				GetComponent<Rigidbody2D>().velocity = new Vector2( -transform.localScale.x * 3f, GetComponent<Rigidbody2D>().velocity.y + 4f );
			}
			shouldKnockBack = false;
		}
	}

	public void ClearPhysics(){
		// DIY Joe physics
		acceleration = new Vector2(0f, 0f);
		velocity = new Vector2(0f, 0f);
		// Unity Physics
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
	}

	public void ClearBlockers(){
		isGrounded 	= false;
		leftLocked	= false;
		rightLocked	= false;
	}

	public void StopAllMotion(){
		isMovingLeft = false;
		isMovingRight = false;
		isJumping = false;
		ClearBlockers();
		ClearPhysics();
	}

}