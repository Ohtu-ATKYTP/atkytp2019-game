using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRank : MonoBehaviour {
    private WebServiceScript webScript;
    private Text rankText;
    void Start() {
        this.rankText = GetComponent<Text>();
        rankText.text = "Rank: ";
    }
    void Update() {
        rankText.text = "Rank: " + PlayerPrefs.GetInt("rank");
    }
}
