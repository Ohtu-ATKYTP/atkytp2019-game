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

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("MainMenu");
        }
        // A Test behaves as an ordinary method

        [UnityTest]
        public IEnumerator ATestInBetweenGameScreenIsLoaded()
        {

            yield return null;
            GameManager.endMinigame(true);
            yield return null;
            Scene scene = SceneManager.GetSceneByName("BetweenGameScreen");
            yield return null;
            Assert.That(scene.name, Is.EqualTo("BetweenGameScreen"));
        }



        [UnityTest]
        public IEnumerator TestScore()
        {   
            yield return new WaitForSeconds(1);
            yield return null;
            //---------------------------------------------------------------
            Debug.Log("Score: " + DataController.GetCurrentScore());
            DataController.AddCurrentScore(10);
            yield return null;
            Debug.Log("Score: " + DataController.GetCurrentScore());
            DataController.AddCurrentScore(10);
            yield return null;
            Debug.Log("Score: " + DataController.GetCurrentScore());
            DataController.AddCurrentScore(0);
            yield return null;
            Debug.Log("Score: " + DataController.GetCurrentScore());
            DataController.AddCurrentScore(30);
            yield return null;
            Debug.Log("Score: " + DataController.GetCurrentScore());
            Assert.That(DataController.GetCurrentScore(), Is.EqualTo(50));

        }

        [TearDown]
        public void TearDown()
        {
            //UnloadAllScenesExcept("");
        }

    }
}
