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
    public GameObject bossHealth;
    public GameObject counts;


    private void Initialization()
    {
        SpawnEnemy.BossArrived += BossUi;
    }

    private void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayUi();
        BossUi();
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void DisplayUi()
    {
        waveDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().waveNumber.ToString();
        enemiesDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().enemiesCount.ToString();
        healthText.text = player.GetComponent<Status>().health.ToString();
    }

    private void BossUi()
    {
        if(boss != null)
        {
            bossHealth.SetActive(true);
            counts.SetActive(false);
            healthBoss.fillAmount = boss.GetComponent<Status>().health / 30 ;
        }
        else
        {
            bossHealth.SetActive(false);
            counts.SetActive(true);
        }
        
    }
}
