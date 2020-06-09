using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTextures : MonoBehaviour {
    public SpriteSet[] blockTextures;

    public Sprite ReturnBlockSprite(float percent) {
        Sprite returnTexture = blockTextures[0].blockTexture;
        for (int i = 0; i < blockTextures.Length; i++) {
            if (percent * 100f <= blockTextures[i].maxHpPercentage) {
                returnTexture = blockTextures[i].blockTexture;
                break;
            }
        }

        return returnTexture;
    }
}

[System.Serializable]
public struct SpriteSet {
    public float maxHpPercentage;
    public Sprite blockTexture;
}