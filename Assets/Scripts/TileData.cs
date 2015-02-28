using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[RequireComponent(typeof(TileMap))]
public class TileData : MonoBehaviour {
	
	public string levelFileName = "level_one.json";
	public bool foreground = true;

	public TileMap tm;

	public static byte[,] tile;
	public int 	width = 3,
				height = 3;

	private string path = "Assets/MapFiles";


	// Use this for initialization
	void Awake () {

		tm = GetComponent<TileMap>();
		tile = new byte[width, height];
		//fillDataWithType((byte)TileTypes.Brick);
		readDataFromJSON( path + "/" + levelFileName );

	}
	
	public void fillDataWithType(byte tileType){
		for (int y=0; y<height; y++){
			for ( int x=0; x<width; x++ ){
				tile[x,y] = tileType;
			}
		}
	}

	public void readDataFromJSON(string FileName){

		string text = System.IO.File.ReadAllText(FileName);

		Map thisLevel = new Map();

		thisLevel = JsonConvert.DeserializeObject<Map>(text);

		width = thisLevel.width;
		height = thisLevel.height;
		
		tile = new byte[width, height];
		tm.Create();

		// apply the bg
		int count = 0;
		for ( int rows=height-1; rows>=0; rows--){
			foreach( byte el in thisLevel.tileBG[rows] ){
				tile[count%width, count/width] = el;
				count++;
			}
		}

		count = 0;
		for ( int rows=height-1; rows>=0; rows--){
			foreach( byte el in thisLevel.tileFG[rows] ){
				if ( el != (byte)TileTypes.Air ){
					tile[count%width, count/width] = el;
				}
				count++;
			}
		}
		
		// override fg 
//		count = 0;
//		for ( int rows=height-1; rows>=0; rows--){
//			List<object> temp = (List<object>)dict["tileFG"];
//			foreach( long el in (List<object>)temp[rows] ){
//				if ( (byte)(long)el != (byte)TileTypes.Air ){
//					tile[count%width, count/width] = (byte)(long)el;
//				}
//				count++;
//			}
//		}


	}

	public byte getTileAtPosition(int x, int y){
		return tile[x,y];
	}
}

public enum TileTypes : byte {
	Air=0,
	Brick,
	Grass,
	Water,
	Sand,
	Test,
	SmallRedBrick,
	BlackBrickBG
}
