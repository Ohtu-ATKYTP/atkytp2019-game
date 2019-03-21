using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;



namespace Tests {

    public interface Jotain {
        int Luku();
    }

    public class FirstEditTest {
        int x;
        Jotain lol;

        [SetUp]
        public void SetUp() {
            lol = Substitute.For<Jotain>();
            lol.Luku().Returns(666);
            x = 5;
        }
        // A Test behaves as an ordinary method
        [Test]
        public void FirstEditTestSimplePasses() {
            try {
                Assert.AreEqual(666, lol.Luku());
            } catch (System.TypeLoadException e) {
                Debug.LogError(e.Data);
            }
            Assert.AreEqual(x, 5);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator FirstEditTestWithEnumeratorPasses() {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
