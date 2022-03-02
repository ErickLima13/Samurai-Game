using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    public TextMeshProUGUI WaveDisplay;
    public TextMeshProUGUI enemiesDisplay;

    public GameObject spawnEnemy;

    // Update is called once per frame
    void Update()
    {
        DisplayUi();
    }

    private void DisplayUi()
    {
        WaveDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().waveNumber.ToString();
        enemiesDisplay.text = spawnEnemy.GetComponent<SpawnEnemy>().enemiesCount.ToString();
    }
}
