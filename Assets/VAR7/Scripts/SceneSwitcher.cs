using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

namespace DCGI.VAR7 
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
        
        public void GoToMetaAvatarsSDKMirror()
        => ChangeScene(SceneNumber.MetaAvatarsSDKMirror);
        
        public void GoToMetaMovementSDKBodyTracking()
        => ChangeScene(SceneNumber.MetaMovementSDKBodyTracking);
        
        private List<InputDevice> _connectedControllers = new();
        private bool _sceneChangeInitiated = false;
            
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
                if (controller.TryGetFeatureValue(CommonUsages.secondaryButton, out pressed) && pressed)
                    ChangeScene(SceneNumber.MVR7);
            });
        }
        
        private async void ChangeScene(SceneNumber sceneNumber)
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)sceneNumber || _sceneChangeInitiated) return;
            _sceneChangeInitiated = true;
            await SceneManager.LoadSceneAsync((int)sceneNumber);
            _sceneChangeInitiated = false;
            
            if (sceneNumber == SceneNumber.MVR7) 
            {
                Destroy(gameObject);
            }
        }
        
        private void AddController(InputDevice controller) 
        {
            _connectedControllers.Add(controller);
        }
    }
}
