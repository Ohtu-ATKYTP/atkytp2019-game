using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveInfoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     deleteAfterTime(2);
    }

    private async void deleteAfterTime(int seconds) {
        await new WaitForSecondsRealtime(seconds);
        this.gameObject.SetActive(false);
        Debug.Log("enabled false");
    }

}
