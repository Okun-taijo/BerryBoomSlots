using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _volumeParametr;
    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(_volumeParametr, -80f);
        _audioMixer.SetFloat(_volumeParametr, volumeValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
