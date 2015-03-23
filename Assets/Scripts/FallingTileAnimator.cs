using UnityEngine;
using System.Collections;

public class FallingTileAnimator : MonoBehaviour {

	public Animator a;

	// Use this for initialization
	void Start () {
		a = gameObject.GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D col){
		if ( a == null ){
			a = gameObject.GetComponent<Animator>();
		}
		if ( col.gameObject.name == "Player" ){
			a.SetBool("isFalling", true);
		}
	}
}
