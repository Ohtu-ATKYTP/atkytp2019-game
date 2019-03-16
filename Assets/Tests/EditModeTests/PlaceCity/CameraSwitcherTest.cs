using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests {
    public class CameraSwitcherTest {

        [Test]
        public void InitializationCallsCorrectMethods() {
            ICameraController stubController = Substitute.For<ICameraController>();

            MainCameraSwitcherLogic logic = new MainCameraSwitcherLogic();
            logic.SetCameraController(stubController);

            stubController.Received(0).FetchMainCamera();
            stubController.Received(0).FetchCameras();
            stubController.Received(0).FetchAttachedCamera();
            logic.Initialize();
            stubController.Received(1).FetchMainCamera();
            stubController.Received(1).FetchCameras();
            stubController.Received(1).FetchAttachedCamera();
        }

        [Test]
        public void ActivateOnlyCameraActivatesAttachedCameraCorrectly() {
            ICamera cam = Substitute.For<ICamera>();
            cam.enabled.Returns(true);
           
            ICameraController stubController = Substitute.For<ICameraController>();
            stubController.FetchCameras().Returns(new[] { cam });
            stubController.FetchAttachedCamera().Returns(cam);

            MainCameraSwitcherLogic logic = new MainCameraSwitcherLogic();
            logic.SetCameraController(stubController);
            logic.ActivateOnlyCamera(cam);

            stubController.Received(1).FetchCameras();
            Assert.IsTrue(cam.enabled);
        }

        [Test]
        public void ActivateOnlyCameraDoesNotDisableMainCameraIfItIsAttached() { }

        [Test]
        public void ActivateonlyCameraWillDisableCamerasThatAreNotParameter() { }

        [Test]
        public void ResetMainCameraWorksWhenCameraWasNotDisabled() { }

        [Test]
        public void ResetMainCameraWillEnableTheInitialMainCamera() { }

        [Test]
        public void CameraSwitcherTestSimplePasses() {
            // Use the Assert class to test conditions
            Assert.That(true);
        }
    }
}
