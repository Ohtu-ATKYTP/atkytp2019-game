using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    [TestFixture]
    public class GameManagerPlayModeTest
    {
        DataController dataController;

        [UnityTest]
        public IEnumerator SampleTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            SceneManager.LoadScene("SceneManagerScene");
            yield return null;
            this.dataController = GameObject.FindObjectOfType<DataController> () as DataController;
            yield return null;
            dataController.SetStatus(DataController.Status.MINIGAME);
            yield return null;
        }
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator TestThatMenuIsUnloadedWhenStatusToMinigame() // And that some other scene is loaded
        {   
            SceneManager.LoadScene("SceneManagerScene");
            yield return null;
            this.dataController = GameObject.FindObjectOfType<DataController> () as DataController;
            yield return null;

            Scene scene = SceneManager.GetSceneByName("MainMenu");
            yield return null;
            Assert.That(scene.name, Is.EqualTo("MainMenu")); // Checks that main menu is loaded
            Assert.That(SceneManager.sceneCount, Is.EqualTo(2));
            yield return null;
            dataController.SetStatus(DataController.Status.MINIGAME);
            yield return null;
            scene = SceneManager.GetSceneByName("MainMenu");
            yield return null;
            Assert.That(scene.name, Is.EqualTo(null)); // Checks that main menu is not loaded anymore
            Assert.That(SceneManager.sceneCount, Is.EqualTo(2));
        }

        [UnityTest]
        public IEnumerator TestInBetweenGameScreenIsLoaded() {
            SceneManager.LoadScene("SceneManagerScene");
            yield return null;
            this.dataController = GameObject.FindObjectOfType<DataController> () as DataController;
            yield return null;
            dataController.SetStatus(DataController.Status.MINIGAME);
            yield return null;
            dataController.SetStatus(DataController.Status.BETWEEN);
            yield return null;
            Scene scene = SceneManager.GetSceneByName("BetweenGameScreen");
            yield return null;
            Assert.That(scene.name, Is.EqualTo("BetweenGameScreen"));
        }

        [UnityTest]
        public IEnumerator TestGameEndsWhen3LifesTaken() {
            SceneManager.LoadScene("SceneManagerScene");
            yield return null;
            this.dataController = GameObject.FindObjectOfType<DataController> () as DataController;
            yield return null;
            dataController.SetStatus(DataController.Status.MINIGAME);
            yield return null;
            //---------------------------------------------------------------
            dataController.MinigameEnd(false, 0);
            yield return null;
            dataController.MinigameEnd(false, 0);
            yield return null;
            dataController.MinigameEnd(false, 0);
            yield return null;
            Assert.That(dataController.GetLives(), Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator TestScore() {
            SceneManager.LoadScene("SceneManagerScene");
            yield return null;
            this.dataController = GameObject.FindObjectOfType<DataController> () as DataController;
            yield return null;
            dataController.SetStatus(DataController.Status.MINIGAME);
            yield return null;
            //---------------------------------------------------------------
            dataController.MinigameEnd(true, 10);
            yield return null;
            dataController.MinigameEnd(true, 10);
            yield return null;
            dataController.MinigameEnd(false, 0);
            yield return null;
            dataController.MinigameEnd(true, 30);
            yield return null;
            Assert.That(dataController.GetCurrentScore(), Is.EqualTo(50));

        }


    }
}
