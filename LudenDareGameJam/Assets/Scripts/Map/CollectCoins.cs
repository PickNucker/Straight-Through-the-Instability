using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    [SerializeField] AudioTrigger collectSound;


    int coins;
    bool collected;

    private void OnTriggerEnter(Collider collider)
    {
       if(collider.gameObject.tag == "Coin")
        {
            if (collected) return;
            coins = coins +1;
            collectSound.Play(Player.instance.transform.position);
            Debug.Log("Coin collected");
            QuestSystem.instance.AddCoins();
            Destroy(collider.gameObject);
            StartCoroutine(EnableAgain());
            collected = true;
        }
    }

    IEnumerator EnableAgain()
    {
        yield return new WaitForSeconds(.3f);
        collected = false;
    }

    public int GetCoins()
    {
        return coins;
    }
}
