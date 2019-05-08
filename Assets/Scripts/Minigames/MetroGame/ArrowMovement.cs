using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Transform t;
    public Transform r;
    Vector3 unitVector = new Vector3(-1,0,0);
    float state = 0;
    Vector3 initialState;
    bool b = false;
    void Start(){
        initialState = t.position;
    }
    void Update()
    {
        if(state == 0 && !b){
            initialState = t.position;
            unitVector = Vector3.Normalize(r.rotation*unitVector);
            b = true;
        }
        if(state<0.5){
            state+=0.25f*Time.deltaTime;
            state= state*1.05f;
            t.position = t.position+(0.1f*((1.25f+Mathf.Pow(2.5f*state,2)))*state*unitVector);
        }else{
            state = 0;
            unitVector *= -1;
        }
    }
}
