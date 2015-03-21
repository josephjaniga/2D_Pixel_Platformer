using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryDisplay : MonoBehaviour {
		
	public GameObject player;
	public GameObject uiPanel;
	public PlayerInventory pi;

	public GameObject InventoryItemContainerPrefab;

	public List<string> inventoryList;
	
	// Use this for initialization
	void Start () {

		initInventoryDisplay();

	}

	public void RedrawInventoryList(){
		foreach ( Transform child in gameObject.transform ){
			Destroy(child.gameObject);
		}
		for ( int i=0; i<inventoryList.Count; i++ ){
			GameObject temp = Instantiate(InventoryItemContainerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			temp.name = "Inventory_Item_Label_"+i;
			temp.transform.Find("InventoryLabelText").GetComponent<Text>().text = inventoryList[i];
			temp.transform.SetParent(transform);
		}
	}
	
	public void initInventoryDisplay(){
		
		foreach ( Transform child in gameObject.transform ){
			Destroy(child);
		}
		
		if ( name == "PlayerInventoryDisplay" ){
			player = _.player;
		}
		
		pi = player.GetComponent<PlayerInventory>();
		inventoryList = pi.inventory;
		
		for ( int i=0; i<inventoryList.Count; i++ ){
			GameObject temp = Instantiate(InventoryItemContainerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			temp.name = "Inventory_Item_Label_"+i;
			temp.transform.Find("InventoryLabelText").GetComponent<Text>().text = inventoryList[i];
			temp.transform.SetParent(transform);
		}
		
	}
	
}
