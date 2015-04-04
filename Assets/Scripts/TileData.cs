using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[RequireComponent(typeof(TileMap))]
public class TileData : MonoBehaviour {
	
	public string levelFileName = "level_one.json";

	public TileMapLayer layer = TileMapLayer.Foreground;

	public TileMap tm;
	
	public byte[,] tileBG;
	public byte[,] tileDE;
	public byte[,] tileFG;

	public PrefabObject[] stuff;
	public Door[] doors;

	public bool[,] collisionData;
	public int 	width = 3,
				height = 3;

	private string path = "MapFiles";

	//public TextAsset TESTFILE;

	// Use this for initialization
	void Awake () {

		tm = GetComponent<TileMap>();
		tileBG = new byte[width, height];
		tileDE = new byte[width, height];
		tileFG = new byte[width, height];
		collisionData = new bool[width, height];

	}
	
	public void fillDataWithType(byte tileType, TileMapLayer layer = TileMapLayer.Foreground){

		switch(layer){
		default:
		case TileMapLayer.Foreground:
			for (int y=0; y<height; y++){
				for ( int x=0; x<width; x++ ){
					tileFG[x,y] = tileType;
				}
			}
			break;
		case TileMapLayer.Decoration:
			for (int y=0; y<height; y++){
				for ( int x=0; x<width; x++ ){
					tileDE[x,y] = tileType;
				}
			}
			break;
		case TileMapLayer.Background:
			for (int y=0; y<height; y++){
				for ( int x=0; x<width; x++ ){
					tileBG[x,y] = tileType;
				}
			}
			break;
		}

	}

	public void readDataFromJSON( string FileName ){

		if ( FileName == null ){
			FileName = path + "/" + levelFileName;
		}

		string t;
		//TextAsset mapDataFile = Resources.Load<TextAsset>(FileName) as TextAsset;
		Object mapDataFile = Resources.Load(FileName);
		//Debug.Log (mapDataFile);
		TextAsset temp = (TextAsset)mapDataFile;
		t = temp.text;

		Map thisLevel = new Map();

		thisLevel = JsonConvert.DeserializeObject<Map>(t);

		width = thisLevel.width;
		height = thisLevel.height;
		
		tileBG = new byte[width, height];
		tileDE = new byte[width, height];
		tileFG = new byte[width, height];
		collisionData = new bool[width, height];
		tm.Create();

		int count = 0;

		switch(layer){
		default:
		case TileMapLayer.Foreground:
			// apply the foreground
			for ( int rows=height-1; rows>=0; rows--){
				foreach( byte el in thisLevel.tileFG[rows] ){
					if ( el != (byte)TileTypes.Blank ){
						tileFG[count%width, count/width] = el;
						collisionData[count%width, count/width] = true;
					}
					count++;
				}
			}

			// populate the stuff into the foreground data object
			stuff = new PrefabObject[thisLevel.stuff.Length];
			for (int i=0; i<thisLevel.stuff.Length; i++){
				stuff[i] = thisLevel.stuff[i];
			}

			// populate the stuff into the foreground data object
			doors = new Door[thisLevel.doors.Length];
			for (int i=0; i<thisLevel.doors.Length; i++){
				doors[i] = thisLevel.doors[i];
			}

			break;
		case TileMapLayer.Decoration:
			// apply the Decorations
			for ( int rows=height-1; rows>=0; rows--){
				foreach( byte el in thisLevel.tileDE[rows] ){
					tileDE[count%width, count/width] = el;
					count++;
				}
			}
			break;
		case TileMapLayer.Background:
			// apply the background
			for ( int rows=height-1; rows>=0; rows--){
				foreach( byte el in thisLevel.tileBG[rows] ){
					tileBG[count%width, count/width] = el;
					count++;
				}
			}
			//fillDataWithType((byte)16, TileMapLayer.Background);
			break;
		}

	}

	public byte getTileAtPosition(int x, int y, TileMapLayer layer = TileMapLayer.Foreground){

		byte temp;

		switch(layer){
		default:
		case TileMapLayer.Foreground:
			temp = tileFG[x,y];
			break;
		case TileMapLayer.Decoration:
			temp = tileDE[x,y];
			break;
		case TileMapLayer.Background:
			temp = tileBG[x,y];
			break;
		}

		return temp;

	}

	public bool getCollisionAtPosition(int x, int y){
		return collisionData[x,y];
	}
}

public enum TileTypes : byte {
	Blank=0,
	BlankOne,
	BlankTwo,
	BlankThree,
	BlankFour,
	BlankFive,
	BlankSix,
	BlankSeven,
	Air,
	Brick,
	Grass,
	Water,
	Sand,
	Test,
	SmallRedBrick,
	BlackBrickBG
}

public enum TileMapLayer : byte {
	Foreground=0,	// tileFG
	Decoration,		// tileDE
	Background		// tileBG
}