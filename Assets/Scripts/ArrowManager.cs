using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public int summaryArrows;
    [SerializeField] private TextMeshProUGUI _arrowsText;
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private int _arrowsCost;
    [SerializeField] private int _arrowsToBuy;

    private void Start()
    {
        summaryArrows = PlayerPrefs.GetInt("Arrows", 0);
        ChangeArrowText();
    }

    public void SpendArrow()
    {
        summaryArrows--;
        ChangeArrowText();
        SaveArrows();
    }
    private void ChangeArrowText()
    {
        _arrowsText.text = summaryArrows.ToString();
    }
    private void SaveArrows()
    {
        PlayerPrefs.SetInt("Arrows", summaryArrows);
    }
    public void BuyArrow()
    {
        if (_arrowsCost <= _resourcesManager.Coins)
        {
            summaryArrows += _arrowsToBuy;
            _resourcesManager.SpendCoins(_arrowsCost);
            ChangeArrowText();
            _resourcesManager.ChangeCoinCounter();
            SaveArrows();
        }
    }
}
