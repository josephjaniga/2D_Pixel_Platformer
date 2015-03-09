using UnityEngine;
using System.Collections;

public static class _
{
	
	private static GameObject 	_player;
	private static GameObject 	_camera;
	private static GameObject 	_canvasOverlay;
	private static GameObject 	_canvasWorld;
	private static GameObject   _mobsGroup;
	private static GameObject	_levelMapSystem;
	private static GameObject	_doorsGroup;
	private static GameObject	_levelManager;
	private static GameObject	_stuffGroup;
	private static GameObject	_chatManager;

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

	// canvas Screen Space Overlay
	public static GameObject canvasOverlay 	
	{
		get {
			GameObject temp = GameObject.Find("Canvas_Overlay");
			if ( _canvasOverlay == null  ){
				if ( temp == null  ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Canvas_Overlay"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Canvas_Overlay";
					_canvasOverlay = temp;
				}
			}
			return _canvasOverlay;
		}
		set { _canvasOverlay = value; }
	}

	// canvas World Space
	public static GameObject canvasWorld
	{
		get {
			GameObject temp = GameObject.Find("Canvas_World");
			if ( _canvasWorld == null  ){
				if ( temp == null  ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/Canvas_World"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Canvas_World";
					_canvasWorld = temp;
				}
			}
			return _canvasWorld;
		}
		set { _canvasWorld = value; }
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

	// LevelManager
	public static GameObject levelManager
	{
		get {
			GameObject temp = GameObject.Find("LevelManager");
			if ( _levelManager == null  ){
				if ( temp == null  ){
					Debug.LogWarning("Level Manager Not Found... Abandon Ship!");
				} else {
					_levelManager = temp;
				}
			}
			return _levelManager;
		}
		set { _levelManager = value; }
	}

	// stuff group
	public static GameObject stuff 	
	{
		get {
			GameObject temp = GameObject.Find("Stuff");
			if ( _stuffGroup == null  ){
				if ( temp == null  ){
					temp = new GameObject();
					temp.name = "Stuff";
					_stuffGroup = temp;
				}
			}
			return _stuffGroup;
		}
		set { _stuffGroup = value; }
	}

	// chatManager
	public static GameObject chatManager
	{
		get {
			GameObject temp = GameObject.Find("ChatManager");
			if ( _chatManager == null  ){
				if ( temp == null  ){
					temp = GameObject.Instantiate(Resources.Load("Prefabs/ChatManager"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "ChatManager";
					temp.transform.position = new Vector3(0f, 0f, -1f);
					_chatManager = temp;
				}
			}
			return _chatManager;
		}
		set { _chatManager = value; }
	}

	

}