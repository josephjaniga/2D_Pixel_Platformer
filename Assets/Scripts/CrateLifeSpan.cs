using UnityEngine;
using System.Collections;

public class CrateLifeSpan : MonoBehaviour {

	public GameObject woodChipPrefab;

	void Start(){
		woodChipPrefab = Resources.Load ("Prefabs/Interactables/WoodChip") as GameObject;
	}

	public void Die(){

		for( int i=0; i<5; i++ ){
			GameObject temp = Instantiate(woodChipPrefab, transform.position, Quaternion.identity) as GameObject;
			temp.transform.SetParent(_.stuff.transform);
		}

	}

}
