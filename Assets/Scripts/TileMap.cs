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

	void Start(){
		Create();
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
		
		GameObject.Find("Player").rigidbody2D.isKinematic = false;
	}

	void BuildTexture(){
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;

		// create a new texture the size of the mesh
		Texture2D texture = new Texture2D(tileResolution * size_x, tileResolution * size_y);

		int texWidth = size_x * tileResolution;
		int texHeight = size_y * tileResolution;

		for(int y=0; y<size_y; y++){
			for(int x=0; x<size_x; x++){
				//int TileOffset = Random.Range(0,5) * tileResolution;
				Color[] p = terrainTiles.GetPixels(
					((int)td.getTileAtPosition(x,y) % numTilesPerRow ) * tileResolution,			// x start
					(int)Mathf.Floor((int)td.getTileAtPosition(x,y) / numTilesPerRow ) * tileResolution,	// y start
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

}
