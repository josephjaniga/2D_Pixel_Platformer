using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RedHeadAudioManager : MonoBehaviour {
	
	public AudioSource audio;
	
	public AudioClip jumpSound;
	public AudioClip bumpSound;
	public AudioClip deathSound;

	public CharacterMotion cm;

	private bool lastGrounded = false;
	
	void Start(){

		cm = gameObject.GetComponent<CharacterMotion>();
		audio = gameObject.GetComponent<AudioSource>();
		jumpSound = Resources.Load<AudioClip>("SFX/RedHead/RedHeadJump");
		bumpSound = Resources.Load<AudioClip>("SFX/RedHead/RedHeadBump");
		deathSound = Resources.Load<AudioClip>("SFX/RedHead/RedHeadDeath");

	}

	void Update(){

		// jump clip
		if ( cm.isJumping && cm.isGrounded ){
			PlayJumpClip();
		}

		// bump clip
		if ( !lastGrounded && cm.isGrounded ){
			PlayBumpClip();
		}

		lastGrounded = cm.isGrounded;

	}

	public void PlayJumpClip(){
		audio.clip = jumpSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}
	
	public void PlayBumpClip(){
		audio.clip = bumpSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}

	public void PlayDeathClip(){
		audio.clip = deathSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}

}
