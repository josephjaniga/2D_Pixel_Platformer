public class PrefabObject {

	public byte type;
	public int xPosition;
	public int yPosition;

}

// hash map
public enum PrefabObjectTypes : byte {
	RedHead=0,
	Spike,
	Meat
}