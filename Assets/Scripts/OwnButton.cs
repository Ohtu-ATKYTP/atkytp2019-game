using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


// Inheriting should keep all the functioanlity of the button, including the ability to add listeners in the graphical editor
// Could be wise to override whatever function is responsible for adding functions in the editor...
public class OwnButton : Button
{
    // Should the attributes be functions, not scripts? 
    [SerializeField] MonoBehaviour[] scripts; 

     void Start() {
        base.Start(); 
        //then add scripts as callbacks
    }

    public void Random(){ 

        this.onClick.AddListener(null); 
  }
    
    
    

}
