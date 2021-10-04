using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public CollectCoins playercoins;
    public Text scoreText;

    void Update()
    {
        scoreText.text = playercoins.GetCoins().ToString();
    }

}
