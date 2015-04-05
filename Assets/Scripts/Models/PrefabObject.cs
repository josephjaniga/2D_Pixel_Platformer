public class PrefabObject {

	public byte type;
	public int xPosition;
	public int yPosition;

	// float color values
	public float r;
	public float g;
	public float b;

	public string objectName;

}

// hash map
public enum PrefabObjectTypes : byte {
	RedHead=0,
	Spike,
	Meat,
	Key,
	LionBoss,
	FallingTile,
	Boots,
	Dagger,
	Crate
}