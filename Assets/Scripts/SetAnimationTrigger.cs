using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    void Start()
    {
        _animator=FindAnyObjectByType<Animator>();
    }

    void Setter()
    {
        _animator.SetTrigger("Money");
    }
}
