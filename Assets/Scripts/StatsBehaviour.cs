using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatsBehaviour : MonoBehaviour
{
    [SerializeField]
    int health = 100;
    [SerializeField]
    Animator animator;
    [SerializeField]
    UnityEvent OnDie;
    public void TakeDamage(int howMuch)
    {

        health -= howMuch;
        animator.SetTrigger("TakeDamage");

        if (health <= 0)
        {
            OnDie.Invoke();
            Debug.Log("I am Dead");
        }
    }
}
