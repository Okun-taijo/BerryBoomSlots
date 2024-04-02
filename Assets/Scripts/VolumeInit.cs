using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    [SerializeField] private AudioMixer _globalAudioMixer;
    [SerializeField] private string _parametr;
    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(_parametr, -80f);
        _globalAudioMixer.SetFloat(_parametr, volumeValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
