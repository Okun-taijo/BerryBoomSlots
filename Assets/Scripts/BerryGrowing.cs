using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerryGrowing : MonoBehaviour
{
    [SerializeField] private int _neededCoins;
    [SerializeField] private int _currentCoins;
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private DragNDrop _dragableComponent;
    [SerializeField] private Button _clickableObject;
    [SerializeField] private Image _changeableImage; 
    [SerializeField] private AudioSource[] _sounds;
    public GameObject _nextBerry;
    private Coroutine _colorChangeCoroutine;

    public void Start()
    {
        _resourcesManager = FindAnyObjectByType<ResourcesManager>();
        _dragableComponent = GetComponent<DragNDrop>();
    }

    public void OnCoinTaken()
   {
        _currentCoins++;
        _sounds[0].Play();
        if( _currentCoins == _neededCoins)
        {
            OnBerryTap();
        }
   }
    private void OnBerryTap()
    {
        //_dragNDrop.enabled = false;
        _clickableObject.enabled = true;
        _sounds[1].Play();
        StartColorChange();
    }
    public void OnClickBerry()
    {
        _resourcesManager.BoostEnergy(10);
        Destroy(gameObject);
    }
    public void OnClickRainbowBerry()
    {
        _resourcesManager.Coins+=50;
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
        Color startColor = Color.white; 
        Color endColor = new Color(0f, 1f, 0.3098f); 

        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f; 

            _changeableImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        Color tempColor = startColor;
        startColor = endColor;
        endColor = tempColor;

        _colorChangeCoroutine = StartCoroutine(ColorChangeCoroutine());
    }
}
