using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private int _energyCost;
    [SerializeField] private Sprite[] _symbols;
    [SerializeField] private float _spinDuration;
    [SerializeField] private Transform[] _symbolSlots;
    [SerializeField] private int _jackpotThreshold;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Animator _animator;
    private bool isSpinning = false;
    private List<Sprite>[] spinningResults;

    private void Start()
    {
        // Встановлюємо випадкові спрайти для кожного слоту при старті гри
        for (int i = 0; i < _symbolSlots.Length; i++)
        {
            SetRandomSymbol(_symbolSlots[i]);
        }
        _resourcesManager.ChangeEnergyCounter();
        _resourcesManager.ChangeCoinCounter();
        // Ініціалізуємо список результатів для кожного слоту
        spinningResults = new List<Sprite>[_symbolSlots.Length];
        for (int i = 0; i < _symbolSlots.Length; i++)
        {
            spinningResults[i] = new List<Sprite>();
        }


    }

    public void Spin()
    {
        if (!isSpinning && _resourcesManager._energy < _energyCost)
        {
            _sceneLoader.GoToSlotMinigame();
        }
        if (isSpinning || _resourcesManager._energy < _energyCost)
            return;

        _resourcesManager._energy -= _energyCost;

        isSpinning = true;
        for (int i = 0; i < _symbolSlots.Length; i++)
        {
            StartCoroutine(SpinSlot(_symbolSlots[i], i));

        }

    }

    private IEnumerator SpinSlot(Transform slot, int slotIndex)
    {
        float elapsedTime = 0f;
        int symbolIndex = UnityEngine.Random.Range(0, _symbols.Length);
        _audioSources[0].Play();
        while (elapsedTime < _spinDuration)
        {
            // Загальна логіка зміни спрайтів тут
            Sprite symbolSprite = _symbols[UnityEngine.Random.Range(0, _symbols.Length)];
            spinningResults[slotIndex].Add(symbolSprite);
            // Встановлення спрайта для кожного слоту
            slot.GetComponent<SpriteRenderer>().sprite = symbolSprite;
            slot.localScale = new Vector3(0.050733f, 0.03f, 1f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Встановлення кінцевого спрайту після прокрутки
        Sprite finalSymbol = _symbols[UnityEngine.Random.Range(0, _symbols.Length)];
        //Debug.Log(finalSymbol);
        slot.GetComponent<SpriteRenderer>().sprite = finalSymbol;
        spinningResults[slotIndex].Add(finalSymbol);
        //Debug.Log(finalSymbol);

        isSpinning = false;
        _resourcesManager.ChangeEnergyCounter();
        _resourcesManager.SavePlayerPrefs();
        yield return new WaitForSeconds(0.5f);
        CheckJackpot();



    }

    private void CheckJackpot()
    {
        string symbolName1 = _symbolSlots[0].GetComponent<SpriteRenderer>().sprite.name;
        string symbolName2 = _symbolSlots[1].GetComponent<SpriteRenderer>().sprite.name;
        string symbolName3 = _symbolSlots[2].GetComponent<SpriteRenderer>().sprite.name;
        // Перевіряємо, чи всі імена спрайтів однакові
        if (symbolName1 == symbolName3 && symbolName2 != symbolName3)
        {
            _resourcesManager._coins += 20;
            _resourcesManager.ChangeCoinCounter();
            _audioSources[1].Play();
            _animator.SetTrigger("SmallWin");
        }
        if (symbolName1 == symbolName2 && symbolName1 == symbolName3)
        {
            if (symbolName1 == "effects_3")
            {
                _resourcesManager._coins += 500;
                _audioSources[3].Play();
                _animator.SetTrigger("Jackpot");
            }
            else
            {

                _resourcesManager._coins += 100;
                _audioSources[2].Play();
                _animator.SetTrigger("MidWin");
            }
            _resourcesManager.ChangeCoinCounter();
        }

    }



    private void SetRandomSymbol(Transform slot)
    {
        Sprite randomSymbol = _symbols[UnityEngine.Random.Range(0, _symbols.Length)];
        slot.GetComponent<SpriteRenderer>().sprite = randomSymbol;
        slot.localScale = new Vector3(0.050733f, 0.03f, 1f);
    }
}





