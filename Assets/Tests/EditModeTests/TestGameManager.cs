using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

namespace Tests
{
    public class TestGameManager
    {
        GameManager gameManager;
        // A Test behaves as an ordinary method
        [SetUp]
        public void SetUp() {
            var gameObject = new GameObject();
            gameObject.AddComponent<GameManager>();
            this.gameManager = gameObject.GetComponent(typeof(GameManager)) as GameManager;
        }


        /*
        Checks if Scenes defined in GameManager are in build settings. If this fails check that all scenes are defined in GameManagers Game and
         OtherScenesThanGames string arrays and all those are defined in build settings. If this fails in cloud remember to check if all scenes are
         in clouds build setttings
         */
        [Test]
        public void TestAllScenesAreInBuildSettings()
        {
            string[] scenesWantToBuild = this.gameManager.getAllScenes();
            string[] allScenes = getAllScenesInBuildSettings();
            int numberOfScenesInBuildSettings = EditorSceneManager.sceneCountInBuildSettings;

            Assert.That(scenesWantToBuild, Is.EquivalentTo(allScenes));
        }


        private string[] getAllScenesInBuildSettings()
        {
            int numberOfScenes = EditorSceneManager.sceneCountInBuildSettings;
            string[] scenes = new string[numberOfScenes];
            for (int i = 0; i < numberOfScenes; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            }
            return scenes;
        }
    }
}
