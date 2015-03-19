using UnityEngine;
using System.Collections;

public class TriggerAttack : MonoBehaviour {
	
	public int TriggerDamageValue = 1;

	void OnTriggerEnter2D (Collider2D col){
		if ( gameObject.tag != col.gameObject.tag ){
			col.gameObject.SendMessage("TakeDamage", TriggerDamageValue, SendMessageOptions.DontRequireReceiver);	
		}
	}
	
}