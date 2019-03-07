using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestGameManager
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestGameManagerSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        [Test]
        public void TestAllScenesAreInBuildSettings() {
            var gameObject = new GameObject(GameManager);
            GameManager gameManager = FindObjectOfType<GameManager>();
            string[] scenes = gameManager.getScenes();
            int numberOfScenesInBuildSettings = SceneManager.sceneCountInBuildSettings;
            Assert.That(scenes.Length, Is.EqualTo(numberOfScenesInBuildSettings));
            foreach (string scene in scenes) {

            }
        }
    }
}
