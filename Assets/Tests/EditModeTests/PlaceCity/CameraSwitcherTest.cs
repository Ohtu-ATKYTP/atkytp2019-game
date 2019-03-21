using NUnit.Framework;
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
        public void InitializeDoesNotDisableMainCameraIfItIsAttached() {
            ICamera mainCamera = Substitute.For<ICamera>();

            ICameraController stubController = Substitute.For<ICameraController>();
            stubController.FetchCameras().Returns(new[] { mainCamera, Substitute.For<ICamera>(), Substitute.For<ICamera>() });
            stubController.FetchAttachedCamera().Returns(mainCamera);
            stubController.FetchMainCamera().Returns(mainCamera);


            MainCameraSwitcherLogic logic = new MainCameraSwitcherLogic();
            logic.SetCameraController(stubController);
            logic.Initialize();

            mainCamera.DidNotReceive().enabled = false;
        }

        [Test]
        public void ActivateonlyCameraWillDisableCamerasThatAreNotParameter() {
            ICamera cam1 = Substitute.For<ICamera>();
            ICamera cam2 = Substitute.For<ICamera>();
            ICamera cam3 = Substitute.For<ICamera>();
            ICamera attachedCamera = Substitute.For<ICamera>();

            ICameraController stubController = Substitute.For<ICameraController>();
            stubController.FetchCameras().Returns(new[] { cam1, cam2, attachedCamera, cam3 });
            stubController.FetchAttachedCamera().Returns(attachedCamera);

            MainCameraSwitcherLogic logic = new MainCameraSwitcherLogic();
            logic.SetCameraController(stubController);


            cam1.enabled = true;
            cam2.enabled = true;
            cam3.enabled = true;
            attachedCamera.enabled = true;


            Assert.IsTrue(cam1.enabled);
            Assert.IsTrue(cam2.enabled);
            Assert.IsTrue(cam3.enabled);
            Assert.IsTrue(attachedCamera.enabled);

            logic.ActivateOnlyCamera(attachedCamera);


            Assert.IsFalse(cam1.enabled);
            Assert.IsFalse(cam2.enabled);
            Assert.IsFalse(cam3.enabled);
            Assert.IsTrue(attachedCamera.enabled);
        }

        [Test]
        public void ResetMainCameraWillEnableTheInitialMainCamera() {
            ICamera initialCam = Substitute.For<ICamera>(); 
            ICamera sceneCam = Substitute.For<ICamera>(); 


            ICameraController stubController = Substitute.For<ICameraController>();
            stubController.FetchAttachedCamera().Returns(sceneCam);
            stubController.FetchCameras().Returns(new [] { initialCam, sceneCam });
            stubController.FetchMainCamera().Returns(initialCam);

            initialCam.enabled = true;
            sceneCam.enabled = true; 

            MainCameraSwitcherLogic logic = new MainCameraSwitcherLogic(); 
            logic.SetCameraController(stubController);
            logic.Initialize(); 

            Assert.IsFalse(initialCam.enabled);
            Assert.IsTrue(sceneCam.enabled);

            logic.ResetMainCamera(); 


            Assert.IsTrue(initialCam.enabled);
            Assert.IsFalse(sceneCam.enabled);
        }

    }
}
