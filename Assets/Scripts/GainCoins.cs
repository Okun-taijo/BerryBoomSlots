using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainCoins : MonoBehaviour
{
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _sceneAnimator;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _resourcesManager = FindAnyObjectByType<ResourcesManager>();
        _sceneAnimator = GetComponentInParent<Animator>();
    }
    public void OnClick()
    {
        _resourcesManager._coins += 50;
        _resourcesManager.SavePlayerPrefs();
        _resourcesManager.ChangeCoinCounter();
        _animator.SetTrigger("Pop");
        _audioSource.Play();
        _sceneAnimator.SetTrigger("Money");
        Destroy(gameObject, 1.5f);
    }
}
