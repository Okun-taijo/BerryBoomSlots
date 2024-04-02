using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : MonoBehaviour
{
    
    [SerializeField]private float _changeDuration = 0.5f; 
    private float _changeStartTime;
    private Image _changedImage; 
    private Color _defaultColor; 

    void Start()
    {
        _changedImage = GetComponent<Image>();
        _defaultColor = _changedImage.color;
        StartChange();
    }

    void StartChange()
    {
        _changeStartTime = Time.time;
        StartCoroutine(ColorChange());
    }

    IEnumerator ColorChange()
    {

        float progress = 0f;
        while (progress < 1f)
        {
            float elapsedTime = Time.time - _changeStartTime;
            progress = Mathf.Clamp01(elapsedTime / _changeDuration);
            Color newColor = _changedImage.color;
            newColor.a = Mathf.Lerp(0f, 1f, progress);
            _changedImage.color = newColor;
            yield return null;
        }
    }
}

