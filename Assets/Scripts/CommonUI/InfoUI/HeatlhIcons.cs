using UnityEngine.UI;
using UnityEngine;

public class HeatlhIcons : MonoBehaviour
{
    public Image heart_1;
    public Image heart_2;
    public Image heart_3;

    void Start()
    {

        switch (DataController.GetLives()) {
            case 0:
                heart_1.enabled = false;
                heart_2.enabled = false;
                heart_3.enabled = false;
                break;
            case 1:
                heart_1.enabled = true;
                heart_2.enabled = false;
                heart_3.enabled = false;
                break;
            case 2:
                heart_1.enabled = true;
                heart_2.enabled = true;
                heart_3.enabled = false;
                break;
            case 3:
                heart_1.enabled = true;
                heart_2.enabled = true;
                heart_3.enabled = true;
                break;
            
        }
    }

}
