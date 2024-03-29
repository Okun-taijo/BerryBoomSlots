using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerryGrowing : MonoBehaviour
{
    [SerializeField] private int _requiredCoins;
    [SerializeField] private int _currentCoins;
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private DragNDrop _dragNDrop;
    [SerializeField] private Button _clickable;
    [SerializeField] private Image _imageToChange; // Ссылка на изображение, которое будет менять цвет
    [SerializeField] private AudioSource[] _audioSources;
    public GameObject _nextBerry;
    private Coroutine _colorChangeCoroutine;

    public void Start()
    {
        _resourcesManager = FindAnyObjectByType<ResourcesManager>();
        _dragNDrop = GetComponent<DragNDrop>();
    }

    public void OnCoinTaken()
   {
        _currentCoins++;
        _audioSources[0].Play();
        if( _currentCoins == _requiredCoins)
        {
            OnBerryTap();
        }
   }
    private void OnBerryTap()
    {
        //_dragNDrop.enabled = false;
        _clickable.enabled = true;
        _audioSources[1].Play();
        StartColorChange();
    }
    public void OnClickBerry()
    {
        _resourcesManager.RechargeEnergy(10);
        Destroy(gameObject);
    }
    public void OnClickRainbowBerry()
    {
        _resourcesManager._coins+=50;
        Destroy(gameObject);
    }
    public void StartColorChange()
    {
        if (_colorChangeCoroutine == null)
        {
            _colorChangeCoroutine = StartCoroutine(ColorChangeCoroutine());
        }
    }

    public void StopColorChange()
    {
        if (_colorChangeCoroutine != null)
        {
            StopCoroutine(_colorChangeCoroutine);
            _colorChangeCoroutine = null;
        }
    }

    private IEnumerator ColorChangeCoroutine()
    {
        float t = 0f;
        Color startColor = Color.white; // Начальный цвет - белый
        Color endColor = new Color(0f, 1f, 0.3098f); // Конечный цвет - желтый

        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f; // Скорость изменения цвета

            _imageToChange.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        // Меняем местами начальный и конечный цветы для следующего прохода
        Color tempColor = startColor;
        startColor = endColor;
        endColor = tempColor;

        _colorChangeCoroutine = StartCoroutine(ColorChangeCoroutine());
    }
}
