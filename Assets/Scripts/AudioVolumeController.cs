using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _globalAudioMixer;
    [SerializeField] private Slider _volumeSettingsSlider;
    [SerializeField] private string _parametrName;

    private float _volumeValue;
    private const float _multiplier = 20f;
    private void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(_parametrName, Mathf.Log10(_volumeSettingsSlider.value) * _multiplier);
        _volumeSettingsSlider.value = Mathf.Pow(10f, _volumeValue/_multiplier);
        _volumeSettingsSlider.onValueChanged.AddListener(ChangeSound);
    }
    private void ChangeSound(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multiplier;
        _globalAudioMixer.SetFloat(_parametrName,_volumeValue);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_parametrName, _volumeValue);
    }
}
