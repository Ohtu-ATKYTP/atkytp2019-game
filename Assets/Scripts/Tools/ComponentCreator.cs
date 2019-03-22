using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentCreator{
    
    public static T Create<T>() where T : Component{ 
        
        GameObject go = new GameObject();        
        go.AddComponent<T>() ;
        return go.GetComponent<T>();

       }
}
