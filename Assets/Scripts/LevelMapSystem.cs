using UnityEngine;
using System.Collections;

public class LevelMapSystem : MonoBehaviour {

	public static TileData tileData;
	public TileMap Foreground;
	public TileMap Decoration;
	public TileMap Background;
	
	// Use this for initialization
	void Start () {
	
		// get the components needed
		Foreground = GameObject.Find ("TileMapFG").GetComponent<TileMap>();
		Decoration = GameObject.Find ("TileMapDE").GetComponent<TileMap>();
		Background = GameObject.Find ("TileMapBG").GetComponent<TileMap>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
