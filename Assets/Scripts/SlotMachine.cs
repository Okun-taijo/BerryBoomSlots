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
    [SerializeField] private Sprite[] _slotsSymbols;
    [SerializeField] private float _spinDuration;
    [SerializeField] private Transform[] _machineSlots;
    [SerializeField] private int _jackpotPin;
    [SerializeField] private AudioSource[] _sounds;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Animator _winAnimator;
    private bool _isSpinningNow = false;
    private List<Sprite>[] _resultOfSpin;

    private void Start()
    {
        for (int i = 0; i < _machineSlots.Length; i++)
        {
            SetRandomSymbol(_machineSlots[i]);
        }
        _resourcesManager.ChangeEnergyCounter();
        _resourcesManager.ChangeCoinCounter();
        _resultOfSpin = new List<Sprite>[_machineSlots.Length];
        for (int i = 0; i < _machineSlots.Length; i++)
        {
            _resultOfSpin[i] = new List<Sprite>();
        }


    }

    public void SpinMachine()
    {
        if (!_isSpinningNow && _resourcesManager.Energy < _energyCost)
        {
            _sceneLoader.GoToEnergyMinigame();
        }
        if (_isSpinningNow || _resourcesManager.Energy < _energyCost)
            return;

        _resourcesManager.Energy -= _energyCost;

        _isSpinningNow = true;
        for (int i = 0; i < _machineSlots.Length; i++)
        {
            StartCoroutine(SpinSlotCoroutine(_machineSlots[i], i));

        }

    }

    private IEnumerator SpinSlotCoroutine(Transform slot, int slotIndex)
    {
        float spinnerTime = 0f;
        int symbolIndex = UnityEngine.Random.Range(0, _slotsSymbols.Length);
        _sounds[0].Play();
        while (spinnerTime < _spinDuration)
        {
            Sprite symbolSprite = _slotsSymbols[UnityEngine.Random.Range(0, _slotsSymbols.Length)];
            _resultOfSpin[slotIndex].Add(symbolSprite);
            slot.GetComponent<SpriteRenderer>().sprite = symbolSprite;
            slot.localScale = new Vector3(0.050733f, 0.03f, 1f);

            spinnerTime += Time.deltaTime;
            yield return null;
        }
        Sprite finalSymbol = _slotsSymbols[UnityEngine.Random.Range(0, _slotsSymbols.Length)];
        slot.GetComponent<SpriteRenderer>().sprite = finalSymbol;
        _resultOfSpin[slotIndex].Add(finalSymbol);

        _isSpinningNow = false;
        _resourcesManager.ChangeEnergyCounter();
        _resourcesManager.SavePlayerPrefs();
        yield return new WaitForSeconds(0.5f);
        CheckOnJackpot();



    }

    private void CheckOnJackpot()
    {
        string symbolName1 = _machineSlots[0].GetComponent<SpriteRenderer>().sprite.name;
        string symbolName2 = _machineSlots[1].GetComponent<SpriteRenderer>().sprite.name;
        string symbolName3 = _machineSlots[2].GetComponent<SpriteRenderer>().sprite.name;
        if (symbolName1 == symbolName3 && symbolName2 != symbolName3)
        {
            _resourcesManager.Coins += 20;
            _resourcesManager.ChangeCoinCounter();
            _sounds[1].Play();
            _winAnimator.SetTrigger("SmallWin");
        }
        if (symbolName1 == symbolName2 && symbolName1 == symbolName3)
        {
            if (symbolName1 == "effects_3")
            {
                _resourcesManager.Coins += 500;
                _sounds[3].Play();
                _winAnimator.SetTrigger("Jackpot");
            }
            else
            {

                _resourcesManager.Coins += 100;
                _sounds[2].Play();
                _winAnimator.SetTrigger("MidWin");
            }
            _resourcesManager.ChangeCoinCounter();
        }

    }



    private void SetRandomSymbol(Transform slot)
    {
        Sprite randomSymbol = _slotsSymbols[UnityEngine.Random.Range(0, _slotsSymbols.Length)];
        slot.GetComponent<SpriteRenderer>().sprite = randomSymbol;
        slot.localScale = new Vector3(0.050733f, 0.03f, 1f);
    }
}





