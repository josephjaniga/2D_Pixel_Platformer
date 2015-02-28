using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMotion : MonoBehaviour
{

	// Input Interface
	public ICharacterInputTypes type;
	public ICharacterInput characterInput;

	// PHYSICS CONSTANTS
	//private float MAX_Y_VELOCITY = 30f;
	private static float MAX_Y_ACCELERATION = 3f;
	private static float MAX_X_ACCELERATION = 3f;

	private static float MAX_VELOCITY = 15f;
	private static float GRAVITY = -9.8f;

	// running & jumping
	private float runSpeed = MAX_X_ACCELERATION;
	private float jumpForce = 3 * MAX_Y_ACCELERATION;

	public Vector2 acceleration 	= new Vector2(0f, 0f);
	public Vector2 velocity 		= new Vector2(0f, 0f);

	// Motion Target Booleans
	public bool isMovingLeft 	= false;
	public bool isMovingRight 	= false;
	public bool isJumping 		= false;

	// Collision Indicators
	public bool isGrounded 		= false;
	public bool leftLocked		= false;
	public bool rightLocked		= false;

	public bool isKnockingBack = false;

	public float lastJumpTime = 0f;
	public float jumpCoolDown = 0.2f;

	// Use this for initialization
	void Start () {

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
		}

		characterInput.Start();
	}

	// Update is called once per frame
	void Update () {

		// check the Input Interface and Update the Motion Target Booleans
		characterInput.Update();
		
		if ( isGrounded && isKnockingBack ){
			isKnockingBack = false;
		}

	}

	void FixedUpdate(){

		// PHYSICS
	
		// Character Movement
		if ( isMovingLeft ){
			if ( !rightLocked ){
				acceleration.x -= runSpeed;
			} else {
				acceleration.x = 0f;
			}
		} else if ( isMovingRight ){
			if ( !rightLocked ){
				acceleration.x += runSpeed;
			} else {
				acceleration.x = 0f;
			}
		} else {
			acceleration.x = 0f;
		}

		// GRAVITY
		acceleration.y += GRAVITY;

		transform.Translate( velocity * Time.deltaTime );	// Position Change translate based on velocity

		Vector2 oldVelocity = velocity; // Velocoity Change
		velocity = new Vector2(
				Mathf.Clamp(oldVelocity.x + acceleration.x * Time.deltaTime, -MAX_VELOCITY, MAX_VELOCITY),
				Mathf.Clamp(
					oldVelocity.y + acceleration.y * Time.deltaTime,
					-MAX_VELOCITY, 
					MAX_VELOCITY
				)
			);


		velocity.x *= .85f;	// Account for X Drag
		if ( velocity.y > 0f ){
			velocity.y *= .85f;	// 
		}

		if ( isJumping && isGrounded ){
			Jump(true);
			isGrounded = false;
		}

	}


	public void Jump(bool shouldJump = true){
		if ( Time.time >= lastJumpTime + jumpCoolDown ){
			if ( shouldJump ){
				// AN ACTUAL JUMP
				lastJumpTime = Time.time;
				acceleration += new Vector2(0f, jumpForce*5f);
			} else {
				// A JUMP ATTACK BOUNCE
				acceleration += new Vector2(0f, jumpForce*3f);
				//velocity = new Vector2(velocity.x, -velocity.y*1.5f);
			}
		}
	}

	/**
	 * TODO: DOES THIS MAKE SENSE?
	 */

	public void TakeDamage(int DamageAmount){
		if ( !isKnockingBack ){
			KnockBack();
		}
	}

	public void KnockBack(){
		isKnockingBack = true;
		if ( !isGrounded ){
			//rigidbody2D.velocity = new Vector2( -transform.localScale.x * 5f, rigidbody2D.velocity.y/2f + 14f );
			velocity = new Vector2( -transform.localScale.x * 5f, velocity.y/2f + 14f );
		} else {
			//rigidbody2D.velocity = new Vector2( -transform.localScale.x * 3f, rigidbody2D.velocity.y + 4f );
			velocity = new Vector2( -transform.localScale.x * 3f, velocity.y + 4f );
		}

	}

}


public enum ICharacterInputTypes {
	CharacterKeyboardInput,
	CharacterTouchInput,
	RandomAI,
	DumbJumpAI
}

public interface ICharacterInput
{
	void Start();
	void Update();
}

public class CharacterKeyboardInput : ICharacterInput
{
	public CharacterMotion cm;

	public CharacterKeyboardInput(CharacterMotion parentMotion){
		cm = parentMotion;
	}

	public void Start(){

	}

	public void Update(){
		if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
			cm.isMovingLeft = true;
		else 
			cm.isMovingLeft = false;

		if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
			cm.isMovingRight = true;
		else 
			cm.isMovingRight = false;

		if ( Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftAlt) )
			cm.isJumping = true;
		else 
			cm.isJumping = false;
	}
}

public class CharacterTouchInput : ICharacterInput
{
	public CharacterMotion cm;

	public ButtonManager left;
	public ButtonManager right;
	public ButtonManager a;

	public CharacterTouchInput(CharacterMotion parentMotion){
		cm = parentMotion;
		left = GameObject.Find("LeftButton").GetComponent<ButtonManager>();
		right = GameObject.Find("RightButton").GetComponent<ButtonManager>();
		a = GameObject.Find("AButton").GetComponent<ButtonManager>();
	}

	public void Start(){
		left = GameObject.Find("LeftButton").GetComponent<ButtonManager>();
		right = GameObject.Find("RightButton").GetComponent<ButtonManager>();
		a = GameObject.Find("AButton").GetComponent<ButtonManager>();
	}

	public void Update(){

		if ( left.pressed )
			cm.isMovingLeft = true;
		else 
			cm.isMovingLeft = false;

		if ( right.pressed )
			cm.isMovingRight = true;
		else 
			cm.isMovingRight = false;

		if ( a.pressed )
			cm.isJumping = true;
		else 
			cm.isJumping = false;

	}

}

public class RandomAI : ICharacterInput
{
	public CharacterMotion cm;
	public RandomAI(CharacterMotion parentMotion){ cm = parentMotion; }
	public void Start(){ Stop(); }
	public void Update(){}

	public void Stop(){
		cm.isMovingLeft = false;
		cm.isMovingRight = false;
		cm.isJumping = false;
	}
}

public class DumbJumpAI : ICharacterInput
{
	public CharacterMotion cm;
	public DumbJumpAI(CharacterMotion parentMotion){ cm = parentMotion; }
	public void Start(){
		cm.isJumping = true;
		cm.isMovingLeft = false;
		cm.isMovingRight = false;
	}
	public void Update(){
		cm.isJumping = true;
		cm.isMovingLeft = false;
		cm.isMovingRight = false;
	}
}
