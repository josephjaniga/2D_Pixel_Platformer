using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

	public GameObject textX;
	public GameObject textY;
	public GameObject textZ;

	public Text tX;
	public Text tY;
	public Text tZ;

	private float speed = 11f;

	public GameObject canvas;

	public float xClamp;
	public float yClamp;

	public float targetSize;

	public Vector3 targetPosition;

	public GameObject TargetMarker;

	// Use this for initialization
	void Start () {

		if ( textX != null && textY != null && textZ != null ){
			tX = textX.GetComponent<Text>();
			tY = textY.GetComponent<Text>();
			tZ = textZ.GetComponent<Text>();
		}

		targetSize = GameObject.Find("Reticle").GetComponent<RectTransform>().rect.width/2f;

		xClamp = canvas.GetComponent<RectTransform>().rect.width - targetSize;
		yClamp = canvas.GetComponent<RectTransform>().rect.height - targetSize;


		//targetPosition = canvas.transform.position + transform.position;
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		xClamp = canvas.GetComponent<RectTransform>().rect.width - targetSize;
		yClamp = canvas.GetComponent<RectTransform>().rect.height - targetSize;

		float 	targetX = 0f,
				targetY = 0f;

		if ( Input.acceleration.x <= -0.1f ) // Left 
			targetX += -speed;

		if ( Input.acceleration.x >= 0.1f ) // Right
			targetX += speed;

		if ( Input.acceleration.y <= -0.90f ) // down
			targetY += -speed;

		if ( Input.acceleration.y >= -0.80f ) // up
			targetY += speed;

		targetPosition += new Vector3(targetX,targetY,0f);
		TargetMarker.transform.position = targetPosition;
		
		if ( textX != null && textY != null && textZ != null ){
			tX.text = "X:" + Input.acceleration.x;
			tY.text = "Y:" + Input.acceleration.y;
			tZ.text = "Z:" + Input.acceleration.z;
		}

		// clamp target position
		if (targetPosition.x <= targetSize){
			targetPosition = new Vector3(targetSize, targetPosition.y, targetPosition.z);
		}

		if (targetPosition.x >= xClamp){
			targetPosition = new Vector3(xClamp, targetPosition.y, targetPosition.z);
		}

		if (targetPosition.y <= targetSize){
			targetPosition = new Vector3(targetPosition.x, targetSize, targetPosition.z);
		}

		if (targetPosition.y >= yClamp){
			targetPosition = new Vector3(targetPosition.x, yClamp, targetPosition.z);
		}

		// clamp reticle position
		if (transform.position.x <= targetSize){
			transform.position = new Vector3(targetSize, transform.position.y, transform.position.z);
		}

		if (transform.position.x >= xClamp){
			transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
		}

		if (transform.position.y <= targetSize){
			transform.position = new Vector3(transform.position.x, targetSize, transform.position.z);
		}

		if (transform.position.y >= yClamp){
			transform.position = new Vector3(transform.position.x, yClamp, transform.position.z);
		}

		// Lerp
		transform.position = Vector3.Lerp(transform.position, targetPosition, speed*Time.deltaTime/2f);

	}
}
