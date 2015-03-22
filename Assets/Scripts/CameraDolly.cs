using UnityEngine;
using System.Collections;

public class CameraDolly : MonoBehaviour {

	public GameObject target;
	public float size = 8f;
	public float fixedZ = -10f;
	public float fixedY = 8f;

	public bool shouldFixTarget = false;

	private Quaternion rightRotationTarget = Quaternion.Euler(0f, 8f, 0f);
	private Quaternion leftRotationTarget = Quaternion.Euler(0f, -8f, 0f);

	void Start(){

		if ( target == null ){
			target = _.player;
		}

		transform.position = new Vector3(target.transform.position.x, fixedY, fixedZ);

	}

	// Update is called once per frame
	void Update () {

		if ( target != null ){

			if ( shouldFixTarget ){

				Camera.main.orthographic = false;
				Camera.main.fieldOfView = 120;
				Vector3 targetPos = new Vector3(target.transform.position.x, 8f, fixedZ);
				transform.position = Vector3.Lerp(transform.position, targetPos, 10f * Time.deltaTime);

				if ( _.player.GetComponent<CharacterDirectionHandler>().currentlyFacingRight ){
					transform.rotation = Quaternion.Slerp(transform.rotation, rightRotationTarget, 4f * Time.deltaTime);
				} else {
					transform.rotation = Quaternion.Slerp(transform.rotation, leftRotationTarget, 4f * Time.deltaTime);
				}

			} else {
				Camera.main.orthographic = true;
				Camera.main.orthographicSize = size;
				Vector3 whyZero = new Vector3(target.transform.position.x, transform.position.y, fixedZ);
				transform.position = Vector3.Lerp(transform.position, whyZero, 10f * Time.deltaTime);
			}

		} else {
			target = _.player;
		}

	}
}
