using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeatlhIcons : MonoBehaviour
{
    public Image hearth_1;
    public Image hearth_2;
    public Image hearth_3;

    private DataController dataController;

    void Start()
    {
        this.dataController = FindObjectOfType<DataController>();
        if(dataController == null)
        {
            return;
        }

        switch (dataController.GetLives()) {
            case 0:
                hearth_1.enabled = false;
                hearth_2.enabled = false;
                hearth_3.enabled = false;
                break;
            case 1:
                hearth_1.enabled = true;
                hearth_2.enabled = false;
                hearth_3.enabled = false;
                break;
            case 2:
                hearth_1.enabled = true;
                hearth_2.enabled = true;
                hearth_3.enabled = false;
                break;
            case 3:
                hearth_1.enabled = true;
                hearth_2.enabled = true;
                hearth_3.enabled = true;
                break;
            
        }
    }

}
