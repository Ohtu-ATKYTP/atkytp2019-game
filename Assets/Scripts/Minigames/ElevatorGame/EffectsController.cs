using UnityEngine;

public class EffectsController : MonoBehaviour {
    
    private GameObject[] stars;

    void Start() {
        stars = GameObject.FindGameObjectsWithTag("Star");
        foreach(GameObject star in stars){
            star.SetActive(false);
        }
    }
    
    public async void ShowStars(){
        
        foreach(GameObject star in stars){
            star.SetActive(true);
        }
        await new WaitForSecondsRealtime(0.2f);
        foreach(GameObject star in stars){
            star.SetActive(false);
        }
    }
}
