using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayButtonScript : MonoBehaviour {
	private DataController dataController;

    // Start is called before the first frame update
    void Start() {
        dataController = FindObjectOfType<DataController>();
    }

    public void StartGameInDebug() {
		dataController.SetDebugMode(true);
		dataController.SetStatus(DataController.Status.BETWEEN);
	}
}
