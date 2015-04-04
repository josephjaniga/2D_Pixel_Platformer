using UnityEngine;
using System.Collections;

public class CameraDolly : MonoBehaviour {

	public GameObject target;
	public float size = 8f;
	public float fixedZ = -10f;

	public float motionConditional = 1f;
	public float targetZ = -10f;

	public float fixedY = 8f;

	public bool shouldFixTarget = false;

	private Quaternion rightRotationTarget = Quaternion.Euler(0f, 8f, 0f);
	private Quaternion leftRotationTarget = Quaternion.Euler(0f, -8f, 0f);

	void Start(){

		if ( target == null ){
			target = _.player;
		}

		transform.position = new Vector3(target.transform.position.x, fixedY, targetZ);

	}

	// Update is called once per frame
	void Update () {

		if ( target != null ){

			if ( shouldFixTarget ){

				if ( _.player.GetComponent<CharacterMotion>().isMovingLeft || _.player.GetComponent<CharacterMotion>().isMovingRight ){
					motionConditional = 2f;
				} else {
					motionConditional = .75f;
				}

				targetZ = Mathf.Lerp (transform.position.z, fixedZ * motionConditional, 5f * Time.deltaTime);

				Camera.main.orthographic = false;
				Camera.main.fieldOfView = 60;
				Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, targetZ);
				transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);

				if ( _.player.GetComponent<CharacterDirectionHandler>().currentlyFacingRight ){
					transform.rotation = Quaternion.Slerp(transform.rotation, rightRotationTarget, .5f * Time.deltaTime);
				} else {
					transform.rotation = Quaternion.Slerp(transform.rotation, leftRotationTarget, .5f * Time.deltaTime);
				}

			} else {
				Camera.main.orthographic = true;
				Camera.main.orthographicSize = size;
				Vector3 whyZero = new Vector3(target.transform.position.x, transform.position.y, fixedZ);
				transform.position = Vector3.Lerp(transform.position, whyZero, 5f * Time.deltaTime);
			}

		} else {
			target = _.player;
		}

	}
}
