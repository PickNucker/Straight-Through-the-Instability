using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem instance;

    [SerializeField] UnityEvent onBossPassed;

    [SerializeField] UnityEvent disableFinsih;

    [SerializeField] UnityEvent oneStar;
    [SerializeField] UnityEvent twoStar;
    [SerializeField] UnityEvent thirdStar;

    [SerializeField] Slider coinSlider;
    [SerializeField] Slider enemySlider;
    [SerializeField] Slider bossSlider;

    [SerializeField] TMPro.TextMeshProUGUI textCountCoins;
    [SerializeField] TMPro.TextMeshProUGUI textCountEnemy;
    [SerializeField] TMPro.TextMeshProUGUI textCountBoss;
    [SerializeField] TMPro.TextMeshProUGUI scoreResult;


    int coinCount;
    int enemyCount;
    int skullyCount;

    int result = 0;

    bool finish;
    bool bossKilled;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        disableFinsih.Invoke();

        finish = false;
        Time.timeScale = 1f;

        coinCount = 0;
        enemyCount = 0;
        skullyCount = 0;
    }

    
    void Update()
    {
        if (finish)
        {
            Time.timeScale = 0f;
        }

        coinSlider.value = coinCount;
        textCountCoins.text = coinCount + "<#557190>" + " / " + "41";

        enemySlider.value = enemyCount;
        textCountEnemy.text = enemyCount +  "<#557190>" + " / " + "10";

        if (bossKilled)
        {
            bossSlider.value = 1;
            textCountBoss.text = 1 + "<#557190>" + " / " + "1";
        }   
        else
        {
            bossSlider.value = 0;
            textCountBoss.text = 0 + "<#557190>" + " / " + "1";
        }
    }

    public void AddCoins()
    {
        coinCount += 1;
    }

    public void AddEnemy()
    {
        enemyCount += 1;
    }

    public void AddBoss()
    {
        bossKilled = true;
    }

    public void GetResult()
    {
        result = coinCount + enemyCount + (bossKilled ? 19 : 0);
        scoreResult.text = "You achieved a total Score of: " + result.ToString("0");

        if(result >= 65)
        {
            thirdStar.Invoke();
        }
        else if (result < 65 && result >= 35)
        {
            twoStar.Invoke();

        }
        else if (result < 35 && result >= 10)
        {
            oneStar.Invoke();
        }
        else
        {
            // BLub
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void FinishGame()
    {
        GetResult();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        onBossPassed.Invoke();
        finish = true;
        Time.timeScale = 0f;
    }
}
