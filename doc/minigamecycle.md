## Minigamecycle managing
Minigame cycle managing is done in GameManager.cs which uses DataController.cs. Datacontroller is do-not-destroy-on-load object so every class can use its methods (singleton).

# How to user the manager in minigames
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

You don't need to do anything else, the manager will close the level for you.