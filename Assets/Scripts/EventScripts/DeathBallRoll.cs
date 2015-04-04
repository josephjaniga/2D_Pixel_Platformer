using UnityEngine;
using System.Collections;

public class DeathBallRoll : MonoBehaviour {

	public float timer = -1f;

	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(-.35f, 0f, 0f));

		if ( timer != -1f ){
			if ( Time.time >= timer ){
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ( col.name == "Player" ){
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			_.player.transform.FindChild("Adventurer").GetComponent<SpriteRenderer>().enabled = false;
			_.player.GetComponent<PlayerAnimator>().setAllAnimators("isKilled", true);
			GameObject blood = Resources.Load ("Prefabs/AnimatedTiles/BloodSplatter") as GameObject;
			Vector3 bloodPosition = new Vector3( _.player.transform.position.x, 9f, 0f); 
			Instantiate(blood, bloodPosition, Quaternion.identity);
			timer = Time.time + 3f;
			_.player.GetComponent<CharacterMotion>().characterInput.inputLock(false);
			_.player.GetComponent<CharacterMotion>().StopAllMotion();
		}
	}

}
