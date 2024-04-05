using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChangeBerry : MonoBehaviour
{
    [SerializeField] private GameObject _berryBank;

   public void NextBankSlot()
   {
        if (_berryBank.transform.localPosition.x > -4000)
        {
            _berryBank.transform.localPosition -= new Vector3(1000f, 0f, 0f);
        }
        else
        {
            _berryBank.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
   }
   public void PreviousBankSlot()
   {
        if (_berryBank.transform.localPosition.x < 0)
        {
            _berryBank.transform.localPosition += new Vector3(1000f, 0f, 0f);
        }
        else
        {
            _berryBank.transform.localPosition = new Vector3(-4000f, 0f, 0f);
        }
    }
}
