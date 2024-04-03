using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUseController : MonoBehaviour
{
    [SerializeField] private ArrowManager _arrowManager;
    [SerializeField] private int _requireArrows;
    [SerializeField] private GameObject _arrowTarget;
    [SerializeField] private Rigidbody2D _arrowTargetRigibody;
    [SerializeField] private GainEnergy _energyGainer;
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _audioSource;

    public void OnClickArrow()
    {
        if (_arrowManager.summaryArrows>0)
        {
            _audioSource.Play();
            _requireArrows--;
            _arrowManager.SpendArrow();
            if (_requireArrows == 0)
            {
                _arrowTargetRigibody.gravityScale = 1;
                _energyGainer.GainBerryEnergy();
                Destroy(_arrowTarget, 1.5f);
                _button.interactable = false;
            }
        }
    }
}
