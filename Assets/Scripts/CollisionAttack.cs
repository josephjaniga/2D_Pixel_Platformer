using UnityEngine;
using System.Collections;

public class CollisionAttack : MonoBehaviour {

	public int CollisionDamageValue = 1;

	void OnCollisionEnter2D (Collision2D col){
		col.gameObject.SendMessage("TakeDamage", CollisionDamageValue, SendMessageOptions.DontRequireReceiver);
	}

}