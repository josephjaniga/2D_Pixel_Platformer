using UnityEngine;
using System.Collections;
using System.Timers;

// TODO: Split out Visual Rendering

public class CharacterHealth : MonoBehaviour {

	public int totalHits = 1;
	public int currentHitsRemaining;

	public bool isFlickering = false;
	public int flickerCounter = 0;
	public Color[] flickerColors = new Color[5];
	public float flickerStopTime = 0f;

	public Timer colorTimer;

	public bool isImmune = false;

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
			Destroy(gameObject);
		}
		
		if ( isFlickering ){
			renderer.material.color = flickerColors[flickerCounter];
		}

		if ( Time.time >= flickerStopTime ){
			isFlickering = false;
			renderer.material.color = Color.white;
		}
	}

	public void TakeDamage(int damageAmount){

		if ( !isFlickering ){
			Debug.Log("taking " + damageAmount + " damage");
			if ( !isImmune ){
				currentHitsRemaining -= damageAmount;
			}
			flicker(1f);
		}
		
	}

	public void flicker(float duration = .5f){
		isFlickering = true;
		flickerStopTime = Time.time + duration;
	}

	public void resetColor(){
		renderer.material.color = Color.white;
	}

}
