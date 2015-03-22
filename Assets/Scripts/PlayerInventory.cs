using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Hands {
	Empty=0,
	Dagger
}

public class PlayerInventory : MonoBehaviour {

	public List<string> inventory = new List<string>();
	public GameObject inventoryDisplay;

	public Hands carrying = Hands.Empty;
	
	// passive items
	public bool hasBoots = false;

	// active items
	public bool hasDagger = true;
	
	// abilities
	public bool canAttack = false;
	public bool canJump = false;

	void Start(){
		inventoryDisplay = GameObject.Find("PlayerInventoryDisplay");
	}

	void Update(){

		// tab cycle through Hands
		if ( Input.GetKeyDown(KeyCode.Tab) ){
			int current = (int)carrying;
			int length = Hands.GetNames(typeof(Hands)).Length;
			current++;
			if ( current > length-1 ) {
				current = 0;
			}
			carryItem ((Hands)current);
		}

		// boots handler
		if ( hasBoots ){
			canJump = true;
			_.player.transform.Find("Boots").gameObject.GetComponent<SpriteRenderer>().enabled = true;
		} else {
			canJump = false;
			_.player.transform.Find("Boots").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}

		if ( inventoryDisplay == null ){
			inventoryDisplay = GameObject.Find("PlayerInventoryDisplay");
		}

	}


	/**
	 * WORKING WITH WEAPONS / ITEMS
	 */

	public void emptyHands(){
		// set the hands empty
		carrying = Hands.Empty;
		canAttack = false;

		// turn off the dagger
		_.player.transform.Find("Dagger").gameObject.GetComponent<SpriteRenderer>().enabled = false;

		// turn off all other carried items here...

	}

	public void carryItem(Hands itemEnum){
		switch(itemEnum){
		default:
		case Hands.Empty:
			emptyHands();
			break;
		case Hands.Dagger:
			if ( hasDagger ){
				emptyHands();
				carrying = Hands.Dagger;
				_.player.transform.Find("Dagger").gameObject.GetComponent<SpriteRenderer>().enabled = true;
				canAttack = true;
			}
			break;
		}
	}

	/**
	 *  MANAGING THE INVENTORY ARRAY
	 */ 

	public void addItem(string itemName){
		if ( inventoryDisplay == null ){
			inventoryDisplay = GameObject.Find("PlayerInventoryDisplay");
		} else {
			inventory.Add(itemName);
			inventoryDisplay.SendMessage("RedrawInventoryList", SendMessageOptions.DontRequireReceiver);
		}
	}

	public bool removeItem(string itemName){
		bool result = false;

		if ( inventory.Contains(itemName) ){
			inventory.RemoveAt(inventory.IndexOf(itemName));
			result = true;
			inventoryDisplay.SendMessage("RedrawInventoryList", SendMessageOptions.DontRequireReceiver);
		}

		return result;
	}

	public bool hasItem(string itemName){
		bool result = false;

		if ( inventory.Contains(itemName) ){
			result = true;
		}

		return result;
	}

}
