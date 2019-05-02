using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidLevelDisplayer : MonoBehaviour
{
    private float percentageLeft;
    private SpriteRenderer renderer; 
    void Start() {
        percentageLeft = 1f;
        renderer = GetComponent<SpriteRenderer>(); 
    }


    public void CalculateLevel() {
        percentageLeft = (1.0f * JalluState.remainingDrops) / JalluState.startingDrops;
        
    }

}
