using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("Audio Mixers")]
        [SerializeField] private AudioMixer _masterAudioMixer;

        [Header("Volume Sliders")]
        [SerializeField] private Slider _masterVolumeSlider;

        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        private void SetMasterVolume(float volume)
        {
            _masterAudioMixer.SetFloat("volume", volume);
        }
    }
}