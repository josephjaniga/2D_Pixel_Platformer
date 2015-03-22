using UnityEngine;
using System.Collections;
using System.Timers;

// TODO: Split out Visual Rendering

public class CharacterHealth : MonoBehaviour {

	// total and current HP
	public int totalHits = 1;
	public int currentHitsRemaining;

	// helpers for the flicker effect on taking damage
	public bool isFlickering = false;
	public int flickerCounter = 0;
	public Color[] flickerColors = new Color[5];
	public float flickerStopTime = 0f;
	public Timer colorTimer;

	// is this character immune to damage
	public bool isImmune = false;
	
	// Player Death Event and Delgate
	public delegate void PlayerDefeat();
	public static event PlayerDefeat e_PlayerDeath;


	// Use this for initialization
	void Start () {

		flickerColors[0] = Color.red;
		flickerColors[1] = Color.green;
		flickerColors[2] = Color.blue;
		flickerColors[3] = Color.yellow;
		flickerColors[4] = Color.black;
		currentHitsRemaining = totalHits;

		colorTimer = new System.Timers.Timer(25);
		colorTimer.Elapsed += delegate {
			flickerCounter++;
			flickerCounter = flickerCounter % 4;
		};
		colorTimer.Start();
	}
	
	// Update is called once per frame
	void Update () {

		if ( currentHitsRemaining <= 0 ){

			// die?
			//gameObject.SetActive(false);
			if ( gameObject.name == "Player" ){
				e_PlayerDeath(); // delegate method to the LevelManager
			} else {

				_.player.SendMessage("ClearBlockers", SendMessageOptions.DontRequireReceiver);

				if ( gameObject.name == "RedHead" ){
					_.player.SendMessage("PlayRedHeadDeathClip", SendMessageOptions.DontRequireReceiver);
				}

				Destroy(gameObject);
			}
		}
		
		if ( isFlickering ){
			GetComponent<Renderer>().material.color = flickerColors[flickerCounter];
		}

		if ( Time.time >= flickerStopTime ){
			isFlickering = false;
			GetComponent<Renderer>().material.color = Color.white;
		}
	}

	public void TakeDamage(int damageAmount){

		if ( !isFlickering ){
			//Debug.Log("taking " + damageAmount + " damage");
			if ( !isImmune ){
				currentHitsRemaining -= damageAmount;
			}
			if ( gameObject.name == "Player" ){
				flicker(1f);
				_.player.SendMessage("PlayHurtClip", SendMessageOptions.DontRequireReceiver);
			} else {
				flicker();
			}

		}
		
	}

	public void Heal(int healAmount){
		if ( currentHitsRemaining + healAmount >= totalHits ) {
			currentHitsRemaining = totalHits;
		} else {
			currentHitsRemaining += healAmount;
		}
	}

	public void flicker(float duration = .33f){
		isFlickering = true;
		flickerStopTime = Time.time + duration;
	}

	public void resetColor(){
		GetComponent<Renderer>().material.color = Color.white;
	}

}
