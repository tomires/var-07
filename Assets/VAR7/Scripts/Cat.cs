using Oculus.Haptics;
using UnityEngine;

namespace DCGI.VAR7 
{
    [RequireComponent(typeof(AudioSource))]
    public class Cat : MonoBehaviour
    {
        [SerializeField] private HapticClip catPurrHapticClip;
        [SerializeField] private AudioClip catPurrAudioClip;
        [SerializeField] private Transform leftController;
        [SerializeField] private Transform rightController;
        
        private static float TRIGGER_DISTANCE = .15f;
        
        private HapticClipPlayer _playerLeft, _playerRight;
        private AudioSource _audioSource;
        
        private bool _playingLeft = false;
        private bool _playingRight = false;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = catPurrAudioClip;
            _playerLeft = new HapticClipPlayer(catPurrHapticClip);
            _playerRight = new HapticClipPlayer(catPurrHapticClip);
            _playerLeft.isLooping = _playerRight.isLooping = true;
        }

        private void Update()
        {
            var triggeredLeft = Vector3.Distance(transform.position, leftController.position) < TRIGGER_DISTANCE;
            if (triggeredLeft && !_playingLeft || !triggeredLeft && _playingLeft) 
                TogglePlayback(ref _playingLeft, _playerLeft, Controller.Left);
                
            var triggeredRight = Vector3.Distance(transform.position, rightController.position) < TRIGGER_DISTANCE;
            if (triggeredRight && !_playingRight || !triggeredRight && _playingRight) 
                TogglePlayback(ref _playingRight, _playerRight, Controller.Right);
        }

        private void OnDestroy()
        {
            _playerLeft.Dispose();
            _playerRight.Dispose();
        }

        private void OnApplicationQuit()
        {
            Haptics.Instance.Dispose();
        }
        
        private void TogglePlayback(ref bool playing, HapticClipPlayer clipPlayer, Controller hand) 
        {
            playing = !playing;
            
            if (playing) 
            {
                clipPlayer.Play(hand);
                _audioSource.Play();
            }
            else 
            {
                clipPlayer.Stop();
                _audioSource.Stop();
            }
        }
    }
}
