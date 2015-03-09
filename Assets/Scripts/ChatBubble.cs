using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatBubble : MonoBehaviour {

	public Text background;
	public Text foreground;

	private float startTime;
	private float fadeAfter = 1f;
	private float lifeTime = 5f;
	private Color colorFG;
	private Color colorBG;

	void Start(){
		startTime = Time.time;
		colorFG = foreground.color;
		colorBG = background.color;
	}

	void Update(){


		if ( Time.time >= startTime + fadeAfter ){

			float currentFadeTime = Time.time - startTime - fadeAfter;
			float totalFadeTime = lifeTime - fadeAfter;

			if ( currentFadeTime < totalFadeTime ){
				colorBG.a = colorFG.a = (totalFadeTime - currentFadeTime) / totalFadeTime;
			} else {
				colorBG.a = colorFG.a = 0f;
			}

			foreground.color = colorFG;
			background.color = colorBG;

		}

		if ( Time.time >= startTime + lifeTime ){
			Destroy(gameObject);
		}



	}
	
}
