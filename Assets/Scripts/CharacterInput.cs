﻿using UnityEngine;
using System.Collections;

public enum ICharacterInputTypes {
	CharacterKeyboardInput,
	CharacterTouchInput,
	RandomAI,
	DumbJumpAI,
	BlankAI,
	LionBossAI
}

public interface ICharacterInput
{
	void Start();
	void Update();
	void inputLock(bool lockStatus = false);
}

public class CharacterKeyboardInput : ICharacterInput
{
	public CharacterMotion cm;

	private float lastAttack = 0f;
	private float attackDurationTime = .48f;
	private float attackCompleteTime = 0f;

	private bool inputLocked = false;

	public void Start(){}

	public CharacterKeyboardInput(CharacterMotion parentMotion){
		cm = parentMotion;
	}
	
	public void Update(){

		if ( !inputLocked ){

			if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
				cm.isMovingLeft = true;
			else 
				cm.isMovingLeft = false;
			
			if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
				cm.isMovingRight = true;
			else 
				cm.isMovingRight = false;
			
			if ( Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftAlt) ){
				// FIXME: making the assumption only the player is keyboard input controlled
				if ( cm.isGrounded ){
					if ( _.playerInventory.canJump ){
						_.player.SendMessage("PlayJumpClip", SendMessageOptions.DontRequireReceiver);
						cm.isJumping = true;
					}
				}
			} else {
				cm.isJumping = false;
			}
			
			// if should attack
			if ( !cm.isAttacking && Input.GetKey(KeyCode.LeftShift) ){
				// FIXME: making the assumption only the player is keyboard input controlled
				if ( Time.time > lastAttack + attackDurationTime ){
					_.player.SendMessage("PlayAttackClip", SendMessageOptions.DontRequireReceiver);
				}
				lastAttack = Time.time;
				attackCompleteTime = Time.time + attackDurationTime;
				cm.isAttacking = true;
			}

		}

		// if the attack animation should end
		if ( Time.time >= attackCompleteTime ) {
			cm.isAttacking = false;
		}
		
	}

	public void inputLock(bool lockStatus = false){ inputLocked = lockStatus; }

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

	public void inputLock(bool inputStatus = false){ }
	
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

	public void inputLock(bool inputStatus = false){}
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
	public void inputLock(bool inputStatus = false){ }
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
	public void Update(){}
	public void inputLock(bool inputStatus=false) {}
}

public class LionBossAI : ICharacterInput
{
	private float lastDirectionChange = 0f;
	private float directionChangeCD = 1f;

	private float lastAttack = 0f;
	private float attackCD = 5f;
	private float attackDurationTime = .8f;
	private float attackCompleteTime = 0f;
	
	public CharacterMotion cm;
	public LionBossAI(CharacterMotion parentMotion){ cm = parentMotion; }

	public void Start(){ 
		lastDirectionChange = Random.Range(0f, 5f);
		lastAttack = Random.Range(0f, 5f);
		Stop();
	}
	
	public void Update(){

		// trigger attack change
		if ( Time.time >= attackCompleteTime ) {
			cm.isAttacking = false;
			if ( Time.time >= lastAttack + attackCD ){
				if ( !cm.isAttacking ) {
					lastAttack = Time.time;
					attackCompleteTime = Time.time + attackDurationTime;
					cm.isAttacking = true;
				}
			}
		}

		// if attacking stop motion
		if ( cm.isAttacking ){
			cm.isMovingLeft = false;
			cm.isMovingRight = false;
		} else {
			// check and change direction
			if ( Time.time >= lastDirectionChange + directionChangeCD ){
				lastDirectionChange = Time.time;
				directionChangeCD = Random.Range(0f, 3f);
				cm.isMovingLeft = false;
				cm.isMovingRight = false;
				ChangeDirection();
			}
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
	
	public void Stop(){
		cm.isMovingLeft = false;
		cm.isMovingRight = false;
		cm.isJumping = false;
	}

	public void inputLock(bool inputStatus = false){}
}