using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlattformChangebyEnemy : MonoBehaviour
{
    [SerializeField] UnityEvent onBossPassed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            QuestSystem.instance.AddEnemy();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Boss")
        {
            QuestSystem.instance.AddBoss();
            Destroy(other.gameObject);
        }
    }

}
