using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMotion : MonoBehaviour
{

	// Input Interface
	public ICharacterInputTypes type;
	public ICharacterInput characterInput;

	// running & jumping
	private float runSpeed = 105f;
	private float jumpForce= 500f;
	private float MAX_Y_VELOCITY = 300f;

	private Vector2 acceleration 	= new Vector2(0f, 0f);
	private Vector2 velocity 		= new Vector2(0f, 0f);

	// Motion Target Booleans
	public bool isMovingLeft 	= false;
	public bool isMovingRight 	= false;
	public bool isJumping 		= false;

	// Collision Indicators
	public bool isGrounded 		= false;
	public bool leftLocked		= false;
	public bool rightLocked		= false;


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

		acceleration = new Vector2(0f,0f);

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
		if ( rigidbody2D.velocity.y >= MAX_Y_VELOCITY )
			rigidbody2D.velocity = new Vector2( rigidbody2D.velocity.x, MAX_Y_VELOCITY );
		if ( rigidbody2D.velocity.y <= -MAX_Y_VELOCITY )
			rigidbody2D.velocity = new Vector2( rigidbody2D.velocity.x, -MAX_Y_VELOCITY );

		transform.Translate(velocity * Time.deltaTime);	// translate based on velocity
		velocity.x *= 0.8f;								// decay x velocity every frame

	}

	public void Jump(bool shouldJump = true){
		if ( shouldJump ){
			rigidbody2D.AddForce(new Vector3(0f, jumpForce, 0f));
		} else {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -rigidbody2D.velocity.y);
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
