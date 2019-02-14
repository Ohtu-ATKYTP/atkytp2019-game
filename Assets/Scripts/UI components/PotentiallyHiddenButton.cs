using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotentiallyHiddenButton : MonoBehaviour
{
    private bool isVisible; 

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Button>().enabled = isVisible;
    }
}
