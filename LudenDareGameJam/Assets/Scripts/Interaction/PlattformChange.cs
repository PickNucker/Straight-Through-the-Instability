using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlattformChange : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") onTriggerPassed.Invoke();
    }
}
