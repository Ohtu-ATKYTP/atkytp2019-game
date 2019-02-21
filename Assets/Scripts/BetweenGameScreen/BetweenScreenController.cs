using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenScreenController : MonoBehaviour
{
	private DataController dataController;
	private float startTime;

    // Start is called before the first frame update
    void Start()
    {
		dataController = FindObjectOfType<DataController>();
        startTime = Time.time;
		Debug.Log("lives" + dataController.GetLives());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 3) {
			dataController.BetweenScreenEnd();
		}
		
    }
}
