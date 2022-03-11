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

    // Update is called once per frame
    void Update()
    {
        DisplayUi();
    }

    private void DisplayUi()
    {
        waveDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().waveNumber.ToString();
        enemiesDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().enemiesCount.ToString();
        healthText.text = player.GetComponent<Status>().health.ToString();
        boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            bossHealth.SetActive(true);
            healthBoss.fillAmount = boss.GetComponent<Status>().health / boss.GetComponent<Skeleton>().health;
            counts.SetActive(false);
        }
        
        else if(boss == null)
        {
            bossHealth.SetActive(false);
            counts.SetActive(true);
        }
    }
}
