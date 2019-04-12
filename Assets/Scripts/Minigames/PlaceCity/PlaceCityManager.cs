using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceCityManager : MonoBehaviour, IMinigameEnder {
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = 1f;
    public int delayAfterMinigameEndsInSeconds = 2;
    public Text organisationText;
    private DataController dataController;
    private GameObject targetCity;
    private bool gameIsOver = false;


    public int difficulty = 1;



    void Start() {
        dataController = FindObjectOfType<DataController>();
        if (dataController != null) {
            difficulty = dataController.GetDifficulty();
        }
        GetComponent<DifficultyAdjuster>().Initialize(difficulty);

#if UNITY_EDITOR
        for (int i = 0; i < locations.Length; i++) {
            locations[i].GetComponent<CircleCollider2D>().radius = 2 * radius;
        }
#endif
        Dictionary<string, string> organisationsByCities = new Dictionary<string, string>(){
                {"Helsinki", "TKO-aly"},
                {"Turku", "Asteriski" },
                {"Tampere", "Luuppi" },
                {"Jyväskylä", "Linkki" },
                {"Kuopio", "Serveri" },
                {"Oulu", "Blanko"}
            };

        targetCity = locations[((int)Random.Range(0f, 6f))].gameObject;
        FindObjectOfType<OrganizationDisplayer>().Initialize(organisationsByCities[targetCity.name]);
    }


    public void handleCityInteraction(GameObject city) {
        if (city != null) {
            if (city == targetCity) {
                WinMinigame();
            } else {
                LoseMinigame();
            }

        } else {
            LoseMinigame();
        }
    }

    public void WinMinigame() {
        if (gameIsOver) {
            return;
        }
        StartCoroutine(EndMinigame(true));
    }

    public void LoseMinigame() {
        if (gameIsOver) {
            return;
        }
        StartCoroutine(EndMinigame(false));
    }


    private IEnumerator EndMinigame(bool win) {
        gameIsOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();


        Color statusColor = win
            ? Color.green
            : Color.red;
        targetCity.GetComponent<InformationDisplayer>().RevealOnMap(statusColor);
        yield return new WaitForSeconds(delayAfterMinigameEndsInSeconds);
        dataController.MinigameEnd(win, win ? 10 : 0);
    }
}