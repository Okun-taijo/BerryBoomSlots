using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private string _volumeParametr;

    private float _volumeValue;
    private const float _multiplier = 20f;
    private void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(_volumeParametr, Mathf.Log10(_volumeSlider.value) * _multiplier);
        _volumeSlider.value = Mathf.Pow(10f, _volumeValue/_multiplier);
        _volumeSlider.onValueChanged.AddListener(ChangeValue);
    }
    private void ChangeValue(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multiplier;
        _audioMixer.SetFloat(_volumeParametr,_volumeValue);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParametr, _volumeValue);
    }
}
