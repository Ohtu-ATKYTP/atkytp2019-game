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

    
    void Awake() {
        finlandRenderer = GetComponent<SpriteRenderer>();
        finlandRenderer.sprite = mapSprites[0];
    }

    // map difficulty need not be the difficulty of the game
    // the decision will be made in the DifficultyAdjuster script
    public void ChangeSprite(int mapDifficulty) {

        int idx = Mathf.Min(mapSprites.Length - 1, mapDifficulty);
        finlandRenderer.sprite = mapSprites[idx];
    }


    public void Rotate(int degrees) {
        transform.Rotate(new Vector3(0, 0, degrees));        
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
        FlipCities(finlandRenderer.flipX, finlandRenderer.flipY);
    }

    private void FlipCities(bool hor, bool vert) {
        Bounds bounds = finlandRenderer.bounds;

        Transform[] cityTransforms = GetComponentsInChildren<Transform>();
        for (int i = 0; i < cityTransforms.Length; i++) {
            if (!cityTransforms[i].gameObject.activeSelf) {
                continue;
            }
            Debug.Log(transform);
            Vector3 center = bounds.center;
            Vector3 additionVector = new Vector3(0, 0, cityTransforms[i].position.z);
            if (hor) {
                additionVector.x += 2 * (bounds.center.x - cityTransforms[i].position.x);
            }

            if (vert) {
                additionVector.y += 2 * (bounds.center.y - cityTransforms[i].position.y);
            }

            cityTransforms[i].position += additionVector;
            Debug.Log(transform.position);
        }


    }



}
