using UnityEngine;
using System.Collections;

public class WoodChipLifeSpan : MonoBehaviour {

	public float lifeSpan = 2f;
	public float timer = -1f;

	// Use this for initialization
	void Start () {
		timer = Time.time + lifeSpan;

		Physics2D.IgnoreCollision( _.player.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>() );
		Physics2D.IgnoreCollision( GameObject.Find ("GroundBumper").GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>() );
		Physics2D.IgnoreCollision( GameObject.Find ("LeftBumper").GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>() );
		Physics2D.IgnoreCollision( GameObject.Find ("RightBumper").GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>() );

		// add some random force
		GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range (-25f, 25f), 25f, 0f));
		GetComponent<Rigidbody2D>().AddTorque(Random.Range (-70f, 70f));
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( timer != -1f ){
			if ( Time.time >= timer ){
				Destroy (gameObject);
			}
		}
	}
}
