using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(TileData))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class TileMap : MonoBehaviour {

	public TileData td;

	public int size_x = 16;
	public int size_y = 16;
	public float tileSize = 1f;

	public Texture2D terrainTiles;
	public int tileResolution = 16;

	public MeshFilter mf;
	public MeshCollider mc;
	public MeshRenderer mr;

	public PolygonCollider2D pc;
	
	public float scale = 1.5f;

	void Start(){
		// defer this functionality to the scene managing classes
		// Create();
	}

	public void Create(){

		if ( mf == null || mc == null || mr == null || pc == null ){
			mf = GetComponent<MeshFilter>();
			mr = GetComponent<MeshRenderer>();
			pc = GetComponent<PolygonCollider2D>();
		}

		td = GetComponent<TileData>();
		size_x = td.width;
		size_y = td.height;

		BuildMesh();
		BuildTexture();

		while ( _.player == null ) {
			_.player.GetComponent<Rigidbody2D>().isKinematic = false;
		}

	}

	void BuildTexture(){

		int numTilesPerRow = terrainTiles.width / tileResolution;

		// create a new texture the size of the mesh
		Texture2D texture = new Texture2D(tileResolution * size_x, tileResolution * size_y);

		for(int y=0; y<size_y; y++){
			for(int x=0; x<size_x; x++){
				//int TileOffset = Random.Range(0,5) * tileResolution;

				int startX = ((int)td.getTileAtPosition(x,y,td.layer) % numTilesPerRow ) * tileResolution;
				int startY = (int)Mathf.Floor((int)td.getTileAtPosition(x,y,td.layer) / numTilesPerRow ) * tileResolution;

				Color[] p = terrainTiles.GetPixels(
					startX,	// x start
					startY,	// y start
					tileResolution,	// width
					tileResolution	// height
					);
				texture.SetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution, p);
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();

		mr.sharedMaterials[0].mainTexture = texture;
	}

	void BuildMesh (){

		int numTiles = size_x * size_y;
		int numTris = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_y = size_y + 1;
		int numVerts = vsize_x * vsize_y;

		// Create the mesh data
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[ numTris * 3 ];

		int x, y, currentIndex = 0;
		// make verts normals and uvs
		for (y=0; y<vsize_y; y++){
			for (x=0; x<vsize_x; x++){
				currentIndex = y * vsize_x + x;
				vertices[ currentIndex ] = new Vector3( x*tileSize, y*tileSize, 0 );
				normals[ currentIndex ] = Vector3.back;
				uv[ currentIndex ] = new Vector2( (float)x/size_x, (float)y/size_y );
			}
		}

		if ( td.layer == TileMapLayer.Foreground ){

			// count the actual collide-able tiles
			int colliderTileCount = 0;
			for (y=0; y<size_y; y++){
				for (x=0; x<size_x; x++){
					if ( td.getCollisionAtPosition(x, y) ) {
						colliderTileCount++;
					}
				}
			}
			// set the number of paths required for the polygon collider
			pc.pathCount = colliderTileCount;

			// make the polygon collider
			colliderTileCount = currentIndex = 0;
			for (y=0; y<size_y; y++){
				for (x=0; x<size_x; x++){
					currentIndex = y * vsize_x + x;
					if ( td.getCollisionAtPosition(x, y) ) {
						Vector2[] pointsArray = new Vector2[4];
						// top left
						pointsArray[0] = new Vector2(x, y+1);
						// top right
						pointsArray[1] = new Vector2(x+1, y+1);
						// bottom right
						pointsArray[2] = new Vector2(x+1, y);
						// bottom left
						pointsArray[3] = new Vector2(x, y);
						pc.SetPath(colliderTileCount, pointsArray);
						colliderTileCount++;
					}
				}
			}
		}
			
		// make the triangles
		for (y=0; y<size_y; y++){
			for (x=0; x<size_x; x++){
				int squareIndex = y * size_x + x;
				int triOffset = squareIndex * 6;

				if ( td.getTileAtPosition(x, y) != (byte)TileTypes.Air ) {
					triangles[triOffset + 0] = y * vsize_x + x + 0;
					triangles[triOffset + 1] = y * vsize_x + x + vsize_x + 0;
					triangles[triOffset + 2] = y * vsize_x + x + vsize_x + 1;

					triangles[triOffset + 3] = y * vsize_x + x + 0;
					triangles[triOffset + 4] = y * vsize_x + x + vsize_x + 1;	
					triangles[triOffset + 5] = y * vsize_x + x + 1;	
				}					
			}
		}

		// Create a new mesh
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		// Assign mesh to the filter renderer
		mf = GetComponent<MeshFilter>();
		mr = GetComponent<MeshRenderer>();
		mf.mesh = mesh;

	}

	public void BuildStuff() {

		// LOAD THE STUFF
		PrefabObject[] stuff = td.stuff;

		for (int i=0; i<stuff.Length; i++){
		
			GameObject temp;
			Vector3 stuffPosition = new Vector3(stuff[i].xPosition, stuff[i].yPosition, 0f) * scale;
			switch( stuff[i].type ){
			default:
				break;
			case (byte)PrefabObjectTypes.RedHead:
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Characters/RedHead"), 
					stuffPosition, 
					Quaternion.identity
					) as GameObject;
				temp.name = "RedHead";
				temp.transform.SetParent(_.mobs.transform);
				break;
			case (byte)PrefabObjectTypes.Spike:
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Interactables/Spike"), 
					stuffPosition,
					Quaternion.identity
					) as GameObject;
				temp.name = "Spike";
				temp.transform.SetParent(_.stuff.transform);
				break;
			case (byte)PrefabObjectTypes.Meat:
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Interactables/Meat"), 
					stuffPosition,
					Quaternion.identity
					) as GameObject;
				temp.name = "Meat";
				temp.transform.SetParent(_.stuff.transform);
				break;
			case (byte)PrefabObjectTypes.Key:
				// if the player doesnt have this key, spawn it in the level
				if ( !_.player.GetComponent<PlayerInventory>().hasItem(stuff[i].objectName) )
                {
					temp = GameObject.Instantiate(
						Resources.Load("Prefabs/Interactables/Key"), 
						stuffPosition,
						Quaternion.identity
						) as GameObject;

					// the object name
					if ( stuff[i].objectName != null ){
						temp.name = stuff[i].objectName;
					} else {
						temp.name = "Key";
					}

					temp.transform.SetParent(_.stuff.transform);
					// the key color
					Color stuffColor = Color.white;
					if ( stuff[i].r != null && stuff[i].g != null && stuff[i].b != null ) {
						stuffColor = new Color(stuff[i].r, stuff[i].g, stuff[i].b);
					}
					temp.GetComponent<SpriteRenderer>().color = stuffColor;
				}
				break;
			case (byte)PrefabObjectTypes.LionBoss:
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Characters/LionBoss"), 
					stuffPosition, 
					Quaternion.identity
					) as GameObject;
				temp.name = "LionBoss";
				temp.transform.SetParent(_.mobs.transform);
				break;
			case (byte)PrefabObjectTypes.FallingTile:
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/AnimatedTiles/FallingTile"), 
					stuffPosition, 
					Quaternion.identity
					) as GameObject;
				temp.name = "FallingTile";
				temp.transform.SetParent(_.stuff.transform);
				break;
			case (byte)PrefabObjectTypes.Boots:
                // if the player doesnt have the boots, spawn it in the level
                if (!_.player.GetComponent<PlayerInventory>().hasItem(stuff[i].objectName))
                {
                    temp = GameObject.Instantiate(
                        Resources.Load("Prefabs/Interactables/Boots"),
                        stuffPosition,
                        Quaternion.identity
                        ) as GameObject;
                    temp.name = "Boots";
                    temp.transform.SetParent(_.stuff.transform);
                }
                break;
			}

		}

	}

	public void BuildDoors() {
		
		Door[] doors = td.doors;
		for (int i=0; i<doors.Length; i++){

			GameObject temp;
			Vector3 doorPosition = new Vector3(doors[i].xPosition, doors[i].yPosition, 0f) * scale;
			Vector3 doorScale = new Vector3(doors[i].doorWidth, doors[i].doorHeight, 1f) * scale;
			Color doorColor = new Color(doors[i].r, doors[i].g, doors[i].b);
			
			switch( doors[i].style ){
			default:
			case "Empty":
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Interactables/EmptyDoor"), 
					doorPosition,
					Quaternion.identity
					) as GameObject;
				temp.name = "EmptyDoor";
				break;
			case "Panel":
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Interactables/PanelDoor"), 
					doorPosition,
					Quaternion.identity
					) as GameObject;
				temp.name = "PanelDoor";
				break;
			case "Temple":
				temp = GameObject.Instantiate(
					Resources.Load("Prefabs/Interactables/TempleDoor"), 
					doorPosition,
					Quaternion.identity
					) as GameObject;
				temp.name = "TempleDoor";
				break;
			}

			temp.transform.SetParent(_.doors.transform);
			temp.transform.localScale = doorScale;
			temp.GetComponent<DoorTraverser>().doorInformation = doors[i];
			temp.GetComponent<SpriteRenderer>().color = doorColor;
		}
		
	}

    public void ResetTileMap(string resetType = "hard")
    {
        Transform stuff = GameObject.Find("Stuff").transform;
        Transform mobs = GameObject.Find("Mobs").transform;
        Transform doors = GameObject.Find("Doors").transform;

        switch (resetType)
        {
            default:
            case "hard":
                // wipe out all objects in the level
                foreach (Transform child in stuff) { Destroy(child.gameObject); }
                foreach (Transform child in mobs) { Destroy(child.gameObject); }
                foreach (Transform child in doors) { Destroy(child.gameObject); }

                // regenerate all objects
                BuildDoors();
                BuildStuff();
                //TODO: make a build mobs function and split them out???

                break;
        }
    }

}
