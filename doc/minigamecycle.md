# Minigamecycle managing
Minigame cycle managing is done in GameManager.cs which uses DataController.cs. Datacontroller is do-not-destroy-on-load object so every class can use its methods (singleton).

## How to use the manager in minigames
When you make your minigame you need the datacontroller:
```C#
private DataController dataController;
```
```C#
private void Start() {
    dataController = FindObjectOfType<DataController>();
}
```
When the minigame is finished you must call MinigameEnd(bool win, int score)
```C#
dataController.MinigameEnd(true, 10);
```
Remember to add your game into the cycle by adding your scenes name to the list in GameManager.cs.

You don't need to do anything else, the manager will close the level for you.
