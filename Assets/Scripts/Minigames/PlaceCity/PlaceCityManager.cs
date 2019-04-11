using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceCityManager : MonoBehaviour, IMinigameEnder {
    // GameObjects representing the cities
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = 1f;
    public int delayAfterMinigameEndsInSeconds = 2;
    public Text organisationText;
    private DataController dataController;
    private GameObject targetCity;
    private Dictionary<string, string> organisationsByCities;
    private bool gameIsOver = false;
    private OrganizationDisplayer orgDisplayer; 



    public int difficulty = 1;



    void Start() {
        dataController = FindObjectOfType<DataController>();
        if (dataController != null) {
            difficulty = dataController.GetDifficulty();
        }
        GetComponent<DifficultyAdjuster>().Initialize(difficulty);

        // No need to show the positions to the players of the production build
        if (Debug.isDebugBuild) {
            for (int i = 0; i < locations.Length; i++) {
                locations[i].GetComponent<CircleCollider2D>().radius = 2 * radius;
            }
        }

        organisationsByCities = new Dictionary<string, string>(){
                {"Helsinki", "TKO-aly"},
                {"Turku", "Asteriski" },
                {"Tampere", "Luuppi" },
                {"Jyväskylä", "Linkki" },
                {"Kuopio", "Serveri" },
                {"Oulu", "Blanko"}
            };

        targetCity = locations[((int)Random.Range(0f, 6f))].gameObject;

        orgDisplayer = FindObjectOfType<OrganizationDisplayer>();
        orgDisplayer.Initialize(organisationsByCities[targetCity.name]);
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
        /*
         * Check for the following situation:  
         * player has clicked the correct city, but the timer runs out. Should the timer stop?
         *
         */
        if (gameIsOver) {
            return;
        }
        StartCoroutine(EndMinigame(false));
    }


    private IEnumerator EndMinigame(bool win) {

        Color statusColor = win
            ? Color.green
            : Color.red;

        gameIsOver = true;

        TimeProgress timerScript = FindObjectOfType<TimeProgress>();
        timerScript.StopTimerProgression();

        targetCity.GetComponent<InformationDisplayer>().RevealOnMap(statusColor);
        yield return new WaitForSeconds(delayAfterMinigameEndsInSeconds);
        dataController.MinigameEnd(win, win ? 10 : 0);
    }
} 