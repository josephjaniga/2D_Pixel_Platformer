using UnityEngine;
using System.Collections;

public class BossHealthDisplay : MonoBehaviour {
	
	public GameObject character;
	public CharacterHealth ch;
	
	public GameObject ResourceContainerPrefab;
	
	public UIResourceContainer[] healthBars;
	
	public ResourceContainerState healthColor = ResourceContainerState.Red;

	private bool initialized = false;
	private bool shouldInitialize = false;

	private bool bossExists = false;

	void Start() {

		if ( character == null ){
			
			// check for bosses to show health
			foreach( Transform mob in _.mobs.transform ){
				if ( mob.name.Contains("Boss") ){
					character = mob.gameObject;
				}
			}
			
			if ( character == null ) {
				foreach ( Transform child in gameObject.transform ){
					Destroy(child.gameObject);
				}
				initialized = false;
				shouldInitialize = false;
			}
			
		} else {

			shouldInitialize = true;
			
		}
		
		if ( !initialized && shouldInitialize ){
			initHealthDisplay();
			initialized = true;
		}
		
		if ( initialized ){
			SetHealth(ch.currentHitsRemaining);
		}

		if ( character != null ){
			bossExists = true;
		} else {
			bossExists = false;
		}

	}

	// Update is called once per frame
	void Update () {
		
		if ( character == null ){
			
			// check for bosses to show health
			foreach( Transform mob in _.mobs.transform ){
				if ( mob.name.Contains("Boss") ){
					character = mob.gameObject;
				}
			}

			if ( character == null ) {
				foreach ( Transform child in gameObject.transform ){
					Destroy(child.gameObject);
				}
				initialized = false;
				shouldInitialize = false;
			}

		} else {
		
			shouldInitialize = true;

		}

		if ( !initialized && shouldInitialize ){
			initHealthDisplay();
			initialized = true;
		}
		
		if ( initialized ){
			SetHealth(ch.currentHitsRemaining);
		}

		if ( bossExists && ch.currentHitsRemaining <= 0 ){
			bossExists = false;
			Application.LoadLevel("TheEnd");
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
