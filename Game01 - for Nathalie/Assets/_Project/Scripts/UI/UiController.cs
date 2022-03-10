using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public TextMeshProUGUI waveDisplay;
    public TextMeshProUGUI enemiesDisplay;
    public TextMeshProUGUI healthText;

    public Image healthBoss;

    public GameObject spawnEnemy;
    public GameObject player;
    public GameObject boss;
    public GameObject counts;

    public GameObject bossHealth;

    private void Initialization()
    {
        SpawnEnemy.BossArrived += BossUi;
    }

    private void Awake()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayUi();
        boss = GameObject.FindGameObjectWithTag("Boss");

    }

    private void DisplayUi()
    {
        waveDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().waveNumber.ToString();
        enemiesDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().enemiesCount.ToString();
        healthText.text = player.GetComponent<Status>().health.ToString();

        if (boss != null)
        {
            healthBoss.fillAmount = boss.GetComponent<Status>().health / 30;
        }
        
        else if(boss == null)
        {
            bossHealth.SetActive(false);
            counts.SetActive(true);
        }
    }

    private void BossUi()
    {
        print("vida do boss");
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossHealth.SetActive(true);
        counts.SetActive(false);
    }
}
