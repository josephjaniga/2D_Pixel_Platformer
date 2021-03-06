﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	/**
	 *  This class is responsible for setting the stage and
	 *  ensuring we have all the objects required to run.
	 */ 

	/**
	 *	TODO: Should this class be responsible for dealing with Player Death?
	 */

	// Game Structure Setup
	public GameObject mainCamera;
	public GameObject player;
	public GameObject mobs;
	public GameObject canvasOverlay;
	public GameObject canvasWorld;
	public GameObject levelMapSystem;
	public GameObject doors;
	public GameObject levelManager;
	public GameObject stuff;
	public GameObject chatManager;

	
	// Level Specific Info
	public string levelDisplayName = "";
	public string levelMapFile = "";
	public Transform startingPosition;

	// Use this for initialization
	void Start () {

		// Prepare the Environment
		init();

		// Load the Map Data & Render the Map - Place STUFF & DOORS
		levelMapSystem.GetComponent<LevelMapSystem>().CreateTileMaps(levelMapFile);

		// Position the Player
		if ( player.GetComponent<CharacterMotion>().shouldUseStartingPosition == true ){
			player.transform.position = startingPosition.position;
		} else {
			player.transform.position = player.GetComponent<CharacterMotion>().targetPosition;
		}

		// make sure the player doesnt spawn motion locked
		player.GetComponent<CharacterMotion>().rightLocked = false;

	}

	void Update (){

		if ( Input.GetKey(KeyCode.Escape) ){
			Application.Quit();
		}

		if ( player.transform.localPosition.y < -10f ){
			PlayerDeath();
		}
	}
	
	public void init (){
		// check for the required game structure
		while ( player == null ){
			player = _.player;
		}

		mainCamera 		= _.camera;
		mobs 			= _.mobs;
		canvasOverlay 	= _.canvasOverlay;
		canvasWorld 	= _.canvasWorld;
		levelMapSystem 	= _.levelMapSystem;
		doors			= _.doors;
		levelManager	= _.levelManager;
		stuff			= _.stuff;
		chatManager		= _.chatManager;
	}


	/**
	 * EVENT DELEGATION!!!
	 */ 

	void OnEnable()
	{
		CharacterHealth.e_PlayerDeath += PlayerDeath;
	}
	
	void OnDisable()
	{
		CharacterHealth.e_PlayerDeath -= PlayerDeath;
	}
	
	public void PlayerDeath()
	{
		CharacterHealth playerCharacterHealth = player.GetComponent<CharacterHealth>();

		// clear flickering
		playerCharacterHealth.isFlickering = false;
		playerCharacterHealth.resetColor();

		// clear velocity
		player.GetComponent<CharacterMotion>().ClearPhysics();

        // FIXME: Reset the level objects from tilemap?
        // TODO: should this be a full or partial reset
        levelMapSystem.GetComponent<LevelMapSystem>().FG_map.ResetTileMap("hard");

        player.transform.position = startingPosition.position;
		playerCharacterHealth.currentHitsRemaining = playerCharacterHealth.totalHits;

	}

}
