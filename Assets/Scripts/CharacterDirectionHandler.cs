using UnityEngine;
using System.Collections;

public class CharacterDirectionHandler : MonoBehaviour {
	
	private CharacterMotion cm;
	private GameObject character;

	public bool defaultLeft = false;
	public Vector3 defaultScale;
	public Vector3 inverseScale;

	public bool currentlyFacingRight = false;
	
	// Use this for initialization
	void Awake () {
		character = gameObject;
		defaultScale = character.transform.localScale;
		inverseScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
		cm = character.GetComponent<CharacterMotion>();
		if ( !defaultLeft ){
			currentlyFacingRight = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if ( !defaultLeft ){
			if ( cm.isMovingLeft )	{
				character.transform.localScale = inverseScale;
				currentlyFacingRight = false;
			}
			if ( cm.isMovingRight )	{
				character.transform.localScale = defaultScale;
				currentlyFacingRight = true;
			}
		} else {
			if ( cm.isMovingRight )	{
				character.transform.localScale = inverseScale;
				currentlyFacingRight = false;
			}
			if ( cm.isMovingLeft )	{
				character.transform.localScale = defaultScale;
				currentlyFacingRight = true;
			}
		}

	}


}
