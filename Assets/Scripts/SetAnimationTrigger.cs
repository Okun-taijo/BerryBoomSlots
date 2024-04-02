using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator _triggerAnimator;
    void Start()
    {
        _triggerAnimator=FindAnyObjectByType<Animator>();
    }

    void Setter()
    {
        _triggerAnimator.SetTrigger("Money");
    }
}
