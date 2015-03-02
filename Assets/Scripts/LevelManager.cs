using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	// Game Structure Setup
	public GameObject camera;
	public GameObject player;
	public GameObject mobs;
	public GameObject canvas;
	public GameObject levelMapSystem;
	public GameObject doors;

	// Level Specific Info
	public string levelDisplayName = "";
	public string levelMapFile = "";
	public bool playerIsDead = false;

	// Use this for initialization
	void Start () {

		// Prepare the Environment
		init();

		// Load the Map Data


		// Render the Map

		// Place the Doors

		// Place the "Stuff"

		// Position the Player


	}
	
	public void init (){

		// check for the required game structure
		player 			= _.player;
		camera 			= _.camera;
		mobs 			= _.mobs;
		canvas 			= _.canvas;
		levelMapSystem 	= _.levelMapSystem;
		doors			= _.doors;

	}
	
}
