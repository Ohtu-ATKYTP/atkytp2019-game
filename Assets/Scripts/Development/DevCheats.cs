using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class DevCheats : MonoBehaviour {
    private bool inMinigame;
    private TimeProgress timer;
    private IMinigameEnder minigameManager;
    private bool minigameEnded;


    private KeyCode[] pauseKeyCodes = {
            KeyCode.Space
        };

    private KeyCode[] winKeyCodes = {
            KeyCode.W,
            KeyCode.Return
        };

    private KeyCode[] loseKeyCodes = {
            KeyCode.L,
            KeyCode.Backspace
        };


    void Start() {
        inMinigame = false;
    }



    public void ConfigureForNonMinigame() {
        inMinigame = false;
        minigameManager = null;
    }


    public async void ConfigureForNewMinigame() {
        inMinigame = true;
        minigameEnded = false; 
        while (!timer || (minigameManager == null)) {

            await Task.Delay(33);
            var x = FindObjectsOfType<MonoBehaviour>().OfType<IMinigameEnder>();
            IEnumerator<IMinigameEnder> enumeraattori = x.GetEnumerator();

            if(enumeraattori.Current != null){
                minigameManager = x.First<IMinigameEnder>();
                timer = FindObjectOfType<TimeProgress>();

            }

        }

    }

    void Update() {
        if (!inMinigame || minigameEnded) {
            return;
        }

        if (KeyIsDown(pauseKeyCodes)) {
            timer.TogglePause();
        } else if (KeyIsDown(winKeyCodes)) {
            minigameEnded = true; 
            minigameManager.WinMinigame();
            
        } else if (KeyIsDown(loseKeyCodes)) {
            minigameEnded = true; 
            minigameManager.LoseMinigame();
        }
    }

    private bool KeyIsDown(KeyCode[] codes) {
        for (int i = 0; i < codes.Length; i++) {
            if (Input.GetKeyDown(codes[i])) {
                return true;
            }
        }
        return false;
    }

}
