public class Door {

	/**
	 * ON MAP INFORMATION
	 */

	// the size of this door in tile units
	public int doorWidth;
	public int doorHeight;
	
	// the position of this door in tile units
	public int xPosition;
	public int yPosition;

	/**
	 * OFF MAP INFORMATION
	 */ 

	// the level this door takes you too
	public string targetScene;

	// the location you come through at on that level
	public int targetPositionX;
	public int targetPositionY;

	// float color values
	public float r;
	public float g;
	public float b;

	public bool isLocked;
	public string itemRequired;

	/*
	 * Empty,
	 * Panel,
	 * Temple
	 */
	public string style;

}