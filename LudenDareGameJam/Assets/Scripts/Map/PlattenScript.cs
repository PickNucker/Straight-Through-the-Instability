using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlattenScript : MonoBehaviour
{
    [SerializeField] int counter = 2;
    [SerializeField] Text count;
    [SerializeField] bool enemyIncluded;
    [SerializeField] bool deactivate;

    bool dead;
    bool touched;

    private void Update()
    {
        if (deactivate)
        {
            count.text = "";
            return;
        }
        count.text = counter.ToString("0");
        if (counter <= 0)
        {
            count.text = "";
            Invoke("Die", .5f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (deactivate) return;


        if (collider.gameObject.name == "Player")
        {
            if (!touched)
            {
                counter -= 1;
                StartCoroutine(SetNewCounter());
                touched = true;
            }
        }


        if (enemyIncluded)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                if (!touched)
                {
                    counter -= 1;
                    StartCoroutine(SetNewCounter());
                    touched = true;
                }
            }
        }
    }

    IEnumerator SetNewCounter()
    {
        yield return new WaitForSeconds(.5f);
        touched = false;
    }

    void Die()
    {
        if (dead) return;
        Destroy(gameObject);
        dead = true;
    }
}
