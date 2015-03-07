using UnityEngine;
using System.Collections;

public class LevelMapSystem : MonoBehaviour {

	public static TileData tileData;

	public GameObject Foreground;
	public GameObject Decoration;
	public GameObject Background;

	public TileData FG_data;
	public TileData DE_data;
	public TileData BG_data;

	public TileMap FG_map;
	public TileMap DE_map;
	public TileMap BG_map;

	public void PrepareTileMaps(){

		// get the components needed
		Foreground = GameObject.Find ("TileMapFG");
		Decoration = GameObject.Find ("TileMapDE");
		Background = GameObject.Find ("TileMapBG");
		
		FG_data = Foreground.GetComponent<TileData>();
		DE_data = Decoration.GetComponent<TileData>();
		BG_data = Background.GetComponent<TileData>();
		
		FG_map = Foreground.GetComponent<TileMap>();
		DE_map = Decoration.GetComponent<TileMap>();
		BG_map = Background.GetComponent<TileMap>();

	}

	public void CreateTileMaps(string MapFileName){

		// if not JSON
		if ( MapFileName == "" ) { MapFileName = "temple_one.json"; }

		PrepareTileMaps();

		// set the level file name on the TileData
		FG_data.levelFileName = DE_data.levelFileName = BG_data.levelFileName = MapFileName;
		
		// tiledata set the data
		FG_data.readDataFromJSON(null);
		DE_data.readDataFromJSON(null);
		BG_data.readDataFromJSON(null);
		
		//tilemap create
		FG_map.Create();
		DE_map.Create();
		BG_map.Create();

		gameObject.transform.localScale = new Vector3(FG_map.scale, FG_map.scale, FG_map.scale);

		// place the STUFF
		FG_map.BuildStuff();

		// place the DOORS
		FG_map.BuildDoors();


	}

}
