using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    public GameObject deathPanel;

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.tag == "Player")
            {
                Time.timeScale = 0f;
                deathPanel.SetActive(true);
            }
        }
    }

}
