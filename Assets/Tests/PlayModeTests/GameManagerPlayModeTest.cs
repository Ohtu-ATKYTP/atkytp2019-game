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
            GameManager.EndMinigame(true);
            yield return null;
            Scene scene = SceneManager.GetSceneByName("BetweenGameScreen");
            yield return null;
            Assert.That(scene.name, Is.EqualTo("BetweenGameScreen"));
        }



        [TearDown]
        public void TearDown()
        {
            //UnloadAllScenesExcept("");
        }

    }
}
