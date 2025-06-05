using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehaviour : MonoBehaviour
{
    [SerializeField]
    string targetTag = "Player";
    [SerializeField]
    int damageAmount = 10; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == targetTag)
        {
            other.GetComponent<StatsBehaviour>().TakeDamage(damageAmount);
        }
    }
}
