using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {
	
	private Canvas canvas;
	public RectTransform leftButton;
	public RectTransform rightButton;
	public RectTransform jumpButton;
	public Vector2 mousePos;

	public int leftTouchCount = 0;
	public int rightTouchCount = 0;
	public int jumpTouchCount = 0;

	public ButtonManager leftBM;
	public ButtonManager rightBM;
	public ButtonManager jumpBM;

	void Start(){
		GameObject left = GameObject.Find("LeftButton");
		GameObject right = GameObject.Find("RightButton");
		GameObject jump = GameObject.Find("AButton");

		leftButton = left.GetComponent<RectTransform>();
		rightButton = right.GetComponent<RectTransform>();
		jumpButton = jump.GetComponent<RectTransform>(); 	

		leftBM = left.GetComponent<ButtonManager>();
		rightBM = right.GetComponent<ButtonManager>();
		jumpBM = jump.GetComponent<ButtonManager>();
	}

	// Update is called once per frame
	void Update () {

		leftTouchCount = 0;
		rightTouchCount = 0;
		jumpTouchCount = 0;

		Rect tempRect;

		foreach ( Touch touch in Input.touches ){
			mousePos = touch.position;

			tempRect = new Rect(leftButton.anchoredPosition.x, leftButton.anchoredPosition.y, leftButton.rect.width, leftButton.rect.height);
			if ( tempRect.Contains(touch.position) ){
				leftTouchCount++;
			}

			tempRect = new Rect(rightButton.anchoredPosition.x, rightButton.anchoredPosition.y, rightButton.rect.width, rightButton.rect.height);
			if ( tempRect.Contains(touch.position) ){
				rightTouchCount++;
			}

			tempRect = new Rect(jumpButton.anchoredPosition.x, jumpButton.anchoredPosition.y, jumpButton.rect.width, jumpButton.rect.height);
			if ( tempRect.Contains(touch.position) ){
				jumpTouchCount++;
			}
		}

		if ( leftTouchCount > 0 ){
			leftBM.pressed = true;
		} else {
			leftBM.pressed = false;
		}

		if ( rightTouchCount > 0 ){
			rightBM.pressed = true;
		} else {
			rightBM.pressed = false;
		}

		if ( jumpTouchCount > 0 ){
			jumpBM.pressed = true;
		} else {
			jumpBM.pressed = false;	
		}

		// mousePos = Input.mousePosition;

		// if ( RectTransformUtility.RectangleContainsScreenPoint(leftButton, mousePos, Camera.main) ){
		// 	Debug.Log("LEFT " + mousePos);
		// }

		// if ( RectTransformUtility.RectangleContainsScreenPoint(rightButton, mousePos, Camera.main) ){
		// 	Debug.Log("RIGHT " + mousePos);
		// }

		// if ( RectTransformUtility.RectangleContainsScreenPoint(jumpButton, mousePos, Camera.main) ){
		// 	Debug.Log("JUMP " + mousePos);
		// }
	}

}
