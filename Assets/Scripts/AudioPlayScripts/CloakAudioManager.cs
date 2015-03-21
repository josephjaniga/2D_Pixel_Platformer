using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CloakAudioManager : MonoBehaviour {

	public AudioSource audio;

	public AudioClip jumpSound;
	public AudioClip bumpSound;
	public AudioClip attackSound;

	public AudioClip itemPickUpSound;
	public AudioClip doorSound;
	public AudioClip redHeadDeathSound;

	void Start(){
		audio = gameObject.GetComponent<AudioSource>();
		jumpSound = Resources.Load<AudioClip>("SFX/Cloak/CloakJump");
		bumpSound = Resources.Load<AudioClip>("SFX/Cloak/CloakBump");
		attackSound = Resources.Load<AudioClip>("SFX/Cloak/CloakAttack");
		itemPickUpSound = Resources.Load<AudioClip>("SFX/PickUpItem");
		doorSound = Resources.Load<AudioClip>("SFX/Door");
		redHeadDeathSound = Resources.Load<AudioClip>("SFX/RedHead/RedHeadDeath");
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
	
	public void PlayAttackClip(){
		audio.clip = attackSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}

	public void PlayItemPickUpClip(){
		audio.clip = itemPickUpSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}

	public void PlayDoorClip(){
		audio.clip = doorSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}

	public void PlayRedHeadDeathClip(){
		audio.clip = redHeadDeathSound;
		if ( audio.isPlaying ){
			audio.Stop();
		}
		audio.Play();
	}

}
