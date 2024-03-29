using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : MonoBehaviour
{
    
    [SerializeField]private float _duration = 0.5f; 
    private float _startTime;
    private Image _image; 
    private Color _startColor; 

    void Start()
    {
        _image = GetComponent<Image>();
        _startColor = _image.color;
        StartFade();
    }

    void StartFade()
    {
        _startTime = Time.time;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {

        float progress = 0f;
        while (progress < 1f)
        {
            float elapsedTime = Time.time - _startTime;
            progress = Mathf.Clamp01(elapsedTime / _duration);
            Color newColor = _image.color;
            newColor.a = Mathf.Lerp(0f, 1f, progress);
            _image.color = newColor;
            yield return null;
        }
    }
}

