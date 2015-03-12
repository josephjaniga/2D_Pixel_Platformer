using UnityEngine;
using System.Collections;

public class CharacterHealthDisplay : MonoBehaviour {

	public GameObject character;
	public GameObject uiPanel;
	public CharacterHealth ch;

	public GameObject ResourceContainerPrefab;

	public UIResourceContainer[] healthBars;

	public ResourceContainerState healthColor = ResourceContainerState.Red;

	// Use this for initialization
	void Start () {

		initHealthDisplay();

	}
	
	// Update is called once per frame
	void Update () {

		if ( character == null ){
			character = _.player;
		} else {
			SetHealth(ch.currentHitsRemaining);
		}

	}

	public void SetHealth(int currentHits){
		for ( int i=0; i< healthBars.Length; i++ ){
			if ( i<currentHits ){
				healthBars[i].state = healthColor;
			} else {
				healthBars[i].state = ResourceContainerState.Empty;
			}
		}
	}

	public void initHealthDisplay(){

		foreach ( Transform child in gameObject.transform ){
			Destroy(child.gameObject);
		}

		if ( name == "PlayerResourcesDisplay" ){
			character = _.player;
		}
		
		ch = character.GetComponent<CharacterHealth>();
		healthBars = new UIResourceContainer[ch.totalHits];

		for ( int i=0; i<ch.totalHits; i++ ){
			GameObject temp = Instantiate(ResourceContainerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			temp.name = "Resource_Bar_"+i;
			temp.transform.SetParent(transform);
			healthBars[i] = temp.GetComponent<UIResourceContainer>();
		}


	}

}
