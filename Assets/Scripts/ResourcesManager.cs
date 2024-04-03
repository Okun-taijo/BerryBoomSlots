using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public int Energy;
    public int Coins;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _energyText;


    public void Start()
    {
        Energy = PlayerPrefs.GetInt("PlayerEnergy", 100);
        EnergyStopper();
        Coins = PlayerPrefs.GetInt("PlayerCoins", 0);
        ChangeCoinCounter();

    }
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayerEnergy", Energy);
        PlayerPrefs.SetInt("PlayerCoins", Coins);
        PlayerPrefs.Save();
    }
    public void ChangeEnergyCounter()
    {
        _energyText.text = Energy.ToString();
    }
    public void ChangeCoinCounter()
    {
        _coinText.text = Coins.ToString();
    }
    public void BoostEnergy(int amount)
    {
        Energy += amount;
        EnergyStopper();
        SavePlayerPrefs();
    }
    public void EnergyStopper()
    {
        if(Energy > _maxEnergy)
        {
            Energy=_maxEnergy;
        }
        ChangeEnergyCounter();
    }
    public void SpendCoins(int amount)
    {
        if (amount <= Coins)
        {
            Coins -= amount;
        }
    }
}
