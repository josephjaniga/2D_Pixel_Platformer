using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

	public List<string> inventory = new List<string>();
	public GameObject inventoryDisplay;

	void Start(){
		inventoryDisplay = GameObject.Find("PlayerInventoryDisplay");
	}

	void Update(){
		if ( inventoryDisplay == null ){
			inventoryDisplay = GameObject.Find("PlayerInventoryDisplay");
		} 
	}

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
