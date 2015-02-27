using UnityEngine;
using System.Collections;

public class CameraDolly : MonoBehaviour {

	public GameObject target;
	public float size = 7f;
	
	// Update is called once per frame
	void Update () {
		Camera.main.orthographicSize = size;
		transform.position = Vector3.Lerp(transform.position, target.transform.position, 10f * Time.deltaTime);
	}
}
