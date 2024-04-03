using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainEnergy : MonoBehaviour
{
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private int _gainingEnergy;
    public void GainBerryEnergy()
    {
        _resourcesManager.BoostEnergy(_gainingEnergy);
        _resourcesManager.SpendCoins(_gainingEnergy);
        _resourcesManager.SavePlayerPrefs();
    }
}
