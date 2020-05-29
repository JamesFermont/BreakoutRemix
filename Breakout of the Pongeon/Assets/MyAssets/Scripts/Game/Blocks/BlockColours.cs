using UnityEngine;

public class BlockColours : MonoBehaviour {
	public ColourSet[] blockColours;

	public Color ReturnBlockColour(float percent) {
		Color returnColour = Color.white;
		if (gameObject.GetComponent<BlockManager>().isImmune) return Color.grey;
		for (int i = 0; i < blockColours.Length; i++) {
			if (percent * 100f <= blockColours[i].maxHpPercentage) {
				returnColour = blockColours[i].blockColour;
				break;
			}
		}
		return returnColour;
	}
}

[System.Serializable]
public struct ColourSet {
	public float maxHpPercentage;
	public Color blockColour;
}