using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _number;
    private int _currentNumber;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator<List<GameObject>> StartCountdown()
    {
        while (true)
        {
            
        }
    }
}
