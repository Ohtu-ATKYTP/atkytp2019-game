using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



/*
 * Only used for storing sprites in a useful format 
 */
public class SpriteManager : MonoBehaviour {
    public Sprite[] mapSprites;
    private SpriteRenderer finlandRenderer;

    
    void Start() {
        finlandRenderer = GetComponent<SpriteRenderer>();
        finlandRenderer.sprite = mapSprites[0];
    }

    // map difficulty need not be the difficulty of the game
    // the decision will be made in the DifficultyAdjuster script
    public void ChangeSprite(int mapDifficulty) {
        int idx = Mathf.Min(mapSprites.Length - 1, mapDifficulty);
        finlandRenderer.sprite = mapSprites[idx];
    }


    public void Flip(int direction) {
        switch(direction){ 
            case 1:
                finlandRenderer.flipX = true;
                break;
            case 2:
                finlandRenderer.flipY = true;
                break;
            case 3:
                finlandRenderer.flipX = true;
                finlandRenderer.flipY = true;
                break;
            default:
                break ;
            }
    }

}
