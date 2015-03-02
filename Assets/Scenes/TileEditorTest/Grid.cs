using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public float widthSpacing = 1.0f;
	public float heightSpacing = 1.0f;

	[Range(32, 256)]
	public int xSize = 4;

	[Range(32, 256)]
	public int ySize = 4;
	
	void OnDrawGizmos()
	{

		Gizmos.color = Color.black;

		// latitudes
		for (float y = 0; y <= ySize; y++)
		{
			Gizmos.DrawLine(new Vector3(0f, y * heightSpacing, 0f), new Vector3(xSize * widthSpacing, y * heightSpacing, 0f));
		}

		Gizmos.color = Color.black;

		// longitudes
		for (float x = 0; x <= xSize; x++)
		{
			Gizmos.DrawLine(new Vector3(x * widthSpacing, 0f, 0f), new Vector3(x * widthSpacing, ySize * heightSpacing, 0f));
		}

	}

}
