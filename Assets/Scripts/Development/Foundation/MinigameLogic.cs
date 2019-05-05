using UnityEngine;
using System.Threading.Tasks;

public abstract class MinigameLogic : MonoBehaviour, IMinigameEnder
{
    [SerializeField]
    protected TimeProgress timebar;

    protected virtual void Start() {
        timebar = FindObjectOfType<TimeProgress>();
        timebar.TimerReadyMethods.RemoveAllListeners();
        timebar.TimerReadyMethods.AddListener(OnTimerEnd);

        ConfigureDifficulty(DataController.GetDifficulty());
    }

    
    protected  virtual async void EndMinigameAsync(bool won) {
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        await DisplayEndingActions(won);
        GameManager.EndMinigame(won);
    }


    public virtual void OnTimerEnd() {
        LoseMinigame();
    }

    protected  virtual async Task DisplayEndingActions(bool won) {
      await new WaitForSeconds(3);
    }


    protected abstract void ConfigureDifficulty(int difficulty);

    #region Implements IMinigameEnder
    public void LoseMinigame() {
        EndMinigameAsync(false);
    }

    public void WinMinigame() {
        EndMinigameAsync(true);
    }
    #endregion

}
