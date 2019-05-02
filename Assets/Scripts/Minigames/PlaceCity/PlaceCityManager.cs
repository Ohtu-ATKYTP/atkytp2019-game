using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityAsyncAwaitUtil;

public class PlaceCityManager : MonoBehaviour, IMinigameEnder
{
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = 1f;
    public int delayAfterMinigameEndsInSeconds = 2;
    public Text organisationText;
    private GameObject targetCity;
    private bool gameIsOver = false;


    public float initialInstructionDuration = 3f;
    public float initialInstructionFadeDuration = 1.5f;

    public int difficulty;



    public async void Start() {
        difficulty = DataController.GetDifficulty();
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
        DisplayInstructionsBeforeGame();
        targetCity = locations[((int)Random.Range(0f, 6f))].gameObject;

        // wait so the mystery remains: what org will I have to place? 
        await new WaitForSecondsRealtime(initialInstructionDuration);

        FindObjectOfType<OrganizationDisplayer>().Initialize(organisationsByCities[targetCity.name]);
    }

    private async void DisplayInstructionsBeforeGame() {
        TimeProgress timer = FindObjectOfType<TimeProgress>();
        timer.TogglePause();
        FadingInstructor fade = FindObjectsOfType<FadingInstructor>().First(fi => fi.name.Equals("Instructions"));
        Time.timeScale = 0f;
        fade.Fade(initialInstructionDuration, initialInstructionFadeDuration);
        await new WaitForSecondsRealtime(initialInstructionDuration + initialInstructionFadeDuration);
        Time.timeScale = 1f;
        timer.TogglePause();
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
        GameManager.endMinigame(win);
    }
}