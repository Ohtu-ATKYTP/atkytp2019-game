using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour{
    private JumpmanManager Jmanager;

    
    void Start() {
        Jmanager = FindObjectOfType<JumpmanManager>();
    }
    public void Jump(){
        Jmanager.Jump();
    }
}
