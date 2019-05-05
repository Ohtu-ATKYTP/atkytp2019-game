using UnityEngine;

public class RemoveInfoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     DeleteAfterTime(2);
    }

    private async void DeleteAfterTime(int seconds) {
        await new WaitForSecondsRealtime(seconds);
        this.gameObject.SetActive(false);
    }

}
