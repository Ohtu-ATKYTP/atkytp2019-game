using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour {
    
    private GameObject[] stars;

    void Start() {
        stars = GameObject.FindGameObjectsWithTag("Star");
        foreach(GameObject star in stars){
            star.SetActive(false);
        }
    }

    void Update() {
        
    }

    public async void ShowStars(){
        //if(!forceDownActive){
        //    return;
        //}

        //forceDownActive = false;

        foreach(GameObject star in stars){
            //if( Random.Range(0,1) > 0.5){
                star.SetActive(true);
            //}
        }
        await new WaitForSecondsRealtime(0.2f);
        foreach(GameObject star in stars){
            star.SetActive(false);
        }
    }
}
