using UnityEngine;
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
	public GameObject camera;
	public GameObject player;
	public GameObject mobs;
	public GameObject canvas;
	public GameObject levelMapSystem;
	public GameObject doors;
	public GameObject levelManager;
	public GameObject stuff;

	
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

		player.GetComponent<CharacterMotion>().rightLocked = false;

	}
	
	public void init (){
		// check for the required game structure
		player 			= _.player;
		camera 			= _.camera;
		mobs 			= _.mobs;
		canvas 			= _.canvas;
		levelMapSystem 	= _.levelMapSystem;
		doors			= _.doors;
		levelManager	= _.levelManager;
		stuff			= _.stuff;
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

		player.transform.position = startingPosition.position;
		playerCharacterHealth.currentHitsRemaining = playerCharacterHealth.totalHits;

	}

}
