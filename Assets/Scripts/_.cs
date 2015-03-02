using UnityEngine;
using System.Collections;

public static class _
{
	
	private static GameObject 	_player;
	private static GameObject 	_camera;
	private static GameObject 	_canvas;
	private static GameObject   _mobsGroup;
	private static GameObject	_levelMapSystem;
	private static GameObject	_doorsGroup;
	
	// player
	public static GameObject player 
	{
		get {
			GameObject temp = GameObject.Find("Player");
			if ( _player == null ){
				if ( temp == null ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Characters/Player"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Player";
					_player = temp;
				}
			}
			return _player;
		}
		set { _player = value; }
	}
	
	// camera
	public static GameObject camera 	
	{
		get {
			GameObject temp = GameObject.Find("MainCamera");
			if ( _camera == null  ){
				if ( temp == null  ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/MainCamera"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "MainCamera";
					_camera = temp;
				}
			}
			return _camera;
		}
		set { _camera = value; }
	}

	// canvas
	public static GameObject canvas 	
	{
		get {
			GameObject temp = GameObject.Find("Canvas");
			if ( _canvas == null  ){
				if ( temp == null  ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Canvas"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Canvas";
					_canvas = temp;
				}
			}
			return _canvas;
		}
		set { _canvas = value; }
	}

	// mobsGroup
	public static GameObject mobs 	
	{
		get {
			GameObject temp = GameObject.Find("Mobs");
			if ( _mobsGroup == null  ){
				if ( temp == null  ){
					temp = new GameObject();
					temp.name = "Mobs";
					_mobsGroup = temp;
				}
			}
			return _mobsGroup;
		}
		set { _mobsGroup = value; }
	}

	// levelMapSystem
	public static GameObject levelMapSystem 	
	{
		get {
			GameObject temp = GameObject.Find("LevelMapSystem");
			if ( _levelMapSystem == null  ){
				if ( temp == null  ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/LevelMapSystem"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "LevelMapSystem";
					_levelMapSystem = temp;
				}
			}
			return _levelMapSystem;
		}
		set { _levelMapSystem = value; }
	}

	// doorsGroup
	public static GameObject doors
	{
		get {
			GameObject temp = GameObject.Find("Doors");
			if ( _doorsGroup == null  ){
				if ( temp == null  ){
					temp = new GameObject();
					temp.name = "Doors";
					_doorsGroup = temp;
				}
			}
			return _doorsGroup;
		}
		set { _doorsGroup = value; }
	}


}