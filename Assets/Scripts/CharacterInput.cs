using UnityEngine;
using System.Collections;

public enum ICharacterInputTypes {
	CharacterKeyboardInput,
	CharacterTouchInput,
	RandomAI,
	DumbJumpAI,
	BlankAI
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
	
	public void Start(){}
	
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
	public float lastDirectionChange = 0f;
	public float directionChangeCD = 1f;
	
	public float lastJumpStatusChange = 0f;
	public float jumpStatusChangeCD = .75f;
	
	public CharacterMotion cm;
	public RandomAI(CharacterMotion parentMotion){ cm = parentMotion; }
	public void Start(){ 
		lastDirectionChange = Random.Range(0f, 5f);
		lastJumpStatusChange = Random.Range(0f, 5f);
		Stop();
	}
	
	public void Update(){

		if ( Time.time >= lastDirectionChange + directionChangeCD ){
			lastDirectionChange = Time.time;
			directionChangeCD = Random.Range(0f, 3f);
			cm.isMovingLeft = false;
			cm.isMovingRight = false;
			ChangeDirection();
		}
		
		if ( Time.time >= lastJumpStatusChange + jumpStatusChangeCD ){
			lastJumpStatusChange = Time.time;
			jumpStatusChangeCD = Random.Range(0f, 2f);
			cm.isJumping = false;
			ChangeJumpStatus();
		}

	}
	
	public void ChangeDirection(){
		int r = Random.Range(0,3);
		switch (r){
		default:
		case 0:
			cm.isMovingLeft = false;
			cm.isMovingRight = false;
			break;
		case 1:
			cm.isMovingLeft = true;
			break;
		case 2:
			cm.isMovingRight = true;
			break;
		}
	}
	
	public void ChangeJumpStatus(){
		int r = Random.Range(0,2);
		switch (r){
		default:
		case 0:
			cm.isJumping = false;
			break;
		case 1:
			cm.isJumping = true;
			break;
		}
	}
	
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

public class BlankAI : ICharacterInput
{
	public CharacterMotion cm;
	public BlankAI(CharacterMotion parentMotion){ cm = parentMotion; }
	public void Start(){
		cm.isJumping = false;
		cm.isMovingLeft = false;
		cm.isMovingRight = false;
	}
	public void Update(){
	}
}