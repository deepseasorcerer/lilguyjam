using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("Audio Mixers")]
        [SerializeField] private AudioMixer _masterAudioMixer;

        [Header("Volume Sliders")]
        [SerializeField] private Slider _mouseSensitivitySlider;

        [SerializeField] private Slider _masterVolumeSlider;

        private PlayerInput _input;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            _mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        }

        private void SetMasterVolume(float volume)
        {
            _masterAudioMixer.SetFloat("volume", volume);
        }

        private void SetMouseSensitivity(float value)
        {
            string newProcessor = $"scaleVector2(x={value},y={value})";

            _input.Player.Look.ApplyBindingOverride(0, new InputBinding { overrideProcessors = newProcessor });
        }
    }
}