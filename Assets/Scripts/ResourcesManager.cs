using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public int _energy;
    public int _coins;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _energyText;
    // Start is called before the first frame update
    public void Start()
    {
        _energy = PlayerPrefs.GetInt("PlayerEnergy", 100);
        EnergyStopper();
        _coins = PlayerPrefs.GetInt("PlayerCoins", 0);
        ChangeCoinCounter();

    }
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayerEnergy", _energy);
        PlayerPrefs.SetInt("PlayerCoins", _coins);
        PlayerPrefs.Save();
    }
    public void ChangeEnergyCounter()
    {
        _energyText.text = _energy.ToString();
    }
    public void ChangeCoinCounter()
    {
        _coinText.text = _coins.ToString();
    }
    public void RechargeEnergy(int amount)
    {
        _energy += amount;
        EnergyStopper();
        SavePlayerPrefs();
    }
    public void EnergyStopper()
    {
        if(_energy > _maxEnergy)
        {
            _energy=_maxEnergy;
        }
        ChangeEnergyCounter();
    }
    public void SpendCoins(int amount)
    {
        if (amount <= _coins)
        {
            _coins -= amount;
        }
    }
}
