using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

namespace DCGI.MVR7 
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void GoToXRHandsHandVisualizer()
            => ChangeScene(SceneNumber.XRHandsHandVisualizer);
            
        public void GoToXRInteractionToolkitHandsDemo()
        => ChangeScene(SceneNumber.XRInteractionToolkitHandsDemo);
        
        public void GoToXRInteractionToolkitDemoScene()
        => ChangeScene(SceneNumber.XRInteractionToolkitDemoScene);
        
        public void GoToMetaXRInteractionSamples()
        => ChangeScene(SceneNumber.MetaXRInteractionSamples);
        
        public void GoToMetaMovementSDKBodyTracking()
        => ChangeScene(SceneNumber.MetaMovementSDKBodyTracking);
        
        private List<InputDevice> _connectedControllers = new();
            
        private void Awake() 
        {
            DontDestroyOnLoad(gameObject);
            
            InputDevices.deviceConnected += AddController;
            var devices = new List<InputDevice>();
            InputDevices.GetDevices(devices);
            devices.ForEach(AddController);
        }
        
        private void Update() 
        {
            _connectedControllers.ForEach(controller => 
            {
                bool pressed;
                if (controller.TryGetFeatureValue(CommonUsages.primaryButton, out pressed) && pressed)
                    ChangeScene(SceneNumber.MVR7);
            });
        }
        
        private void ChangeScene(SceneNumber sceneNumber)
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)sceneNumber) return;
            SceneManager.LoadSceneAsync((int)sceneNumber);
        }
        
        private void AddController(InputDevice controller) 
        {
            _connectedControllers.Add(controller);
        }
    }
}
