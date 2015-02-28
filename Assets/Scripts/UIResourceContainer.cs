using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ResourceContainerState {
	Empty, 
	Red,
	Blue,
	Green,
	Purple,
	Yellow
}

public class UIResourceContainer : MonoBehaviour {

	public ResourceContainerState state = ResourceContainerState.Empty;
	public Image i;

	public Sprite emptySprite;
	public Sprite redSprite;
	public Sprite blueSprite;
	public Sprite greenSprite;
	public Sprite purpleSprite;
	public Sprite yellowSprite;

	// Use this for initialization
	void Start () {
		i = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
		switch (state){
			default:
			case ResourceContainerState.Empty:
				i.sprite = emptySprite;
			break;
			case ResourceContainerState.Red:
				i.sprite = redSprite;
			break;
			case ResourceContainerState.Blue:
				i.sprite = blueSprite;
			break;
			case ResourceContainerState.Green:
				i.sprite = greenSprite;
			break;
			case ResourceContainerState.Yellow:
				i.sprite = yellowSprite;
			break;
			case ResourceContainerState.Purple:
				i.sprite = purpleSprite;
			break;
		}

	}
}
