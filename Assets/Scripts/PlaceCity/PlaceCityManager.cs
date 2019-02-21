using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceCityManager : MonoBehaviour {
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = 1f;
    public int delayAfterMinigameEndsInSeconds = 2;
    public bool drawGizmos = true;
    public Color gizmoColor = Color.cyan;
    public Text organisationText;
    public Text winText;
    public Text loseText;
    private DataController dataController;
    private bool allowTouching;
    private GameObject targetCity;
    private Dictionary<string, string> organisationsByCities;



    void Start() {
        allowTouching = true;
        dataController = FindObjectOfType<DataController>();
        for (int i = 0; i < locations.Length; i++) {
            locations[i].GetComponent<CircleCollider2D>().radius = 2 * radius;
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
        this.SetOnlyCityActive(targetCity);
        organisationText.text = organisationsByCities[targetCity.name];
        winText.enabled = false;
        loseText.enabled = false;

        activateOnlyCurrentSceneCamera();
        Debug.Log("Main kameran nimi: " + Camera.main.name);

    }


    private void activateOnlyCurrentSceneCamera() {
        activateOnlyCamera("PlaceCityCamera");

    }

    private void activateOnlyCamera(string cameraName) {
        Camera[] cameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++) {
            if (cameras[i].name != cameraName) {
                cameras[i].enabled = false;
            } else {
                cameras[i].enabled = true;
            }
        }
    }




    private void SetOnlyCityActive(GameObject city) {
        for (int i = 0; i < locations.Length; i++) {
            if (locations[i].gameObject != city) {
                locations[i].gameObject.SetActive(false);
            }
        }
    }

    private void Initialize() {
        Start();
    }


    void OnDrawGizmos() {
        if (!drawGizmos) {
            return;
        }
        Initialize();
        for (int i = 0; i < locations.Length; i++) {
            Vector3 v = locations[i].position;
            Gizmos.DrawSphere(v, radius);
        }
    }

    void Update() {
        if (!allowTouching) {
            return;
        }
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            // z = distance from camera
            Vector3 pos = new Vector3(touch.position.x, touch.position.y, 0);

            pos = Camera.main.ScreenToWorldPoint(pos);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(pos, Vector2.zero);
            GameObject city = hit ? hit.collider.gameObject : null;

            if (city != null) {
                Debug.Log("HIT! " + city.name);
                if (city == targetCity) {
                    StartCoroutine(EndMinigame(true));
                } else {
                    StartCoroutine(EndMinigame(false));
                }

            } else {
                Debug.Log("HÄVISIT PELIN");
                StartCoroutine(EndMinigame(false));
            }

            targetCity.GetComponent<InformationDisplayer>().DisplayOnMap();
            allowTouching = false;
        }
    }

    public void winMinigame(){ 
            StartCoroutine(EndMinigame(true));
        }

    public void loseMinigame(){ 
            StartCoroutine(EndMinigame(false));
        }


    private IEnumerator EndMinigame(bool win) {

        // jokin ilmoitus loppumisesta
        if (win) {
            winText.enabled = true;
        } else {
            loseText.enabled = true;
        }

        yield return new WaitForSeconds(delayAfterMinigameEndsInSeconds);
        //varmaan hyvä idea lopulta (jos SceneManagerCamera renderöi jotain pelien välissä)
        //activateOnlyCamera("SceneManagerCamera");
        dataController.MinigameEnd(win, win ? 10 : 0);
    }
}



