using UnityEngine;
using System.Collections;

public class CameraDolly : MonoBehaviour {

	public GameObject target;
	public float size = 8f;
	public float fixedZ = -10f;
	public float fixedY = 8f;

	void Start(){

		if ( target == null ){
			target = _.player;
		}

		transform.position = new Vector3(target.transform.position.x, fixedY, fixedZ);

	}

	// Update is called once per frame
	void Update () {

		if ( target != null ){
			Camera.main.orthographicSize = size;
			Vector3 whyZero = new Vector3(target.transform.position.x, transform.position.y, fixedZ);
			transform.position = Vector3.Lerp(transform.position, whyZero, 10f * Time.deltaTime);
		} else {
			target = _.player;
		}

	}
}
