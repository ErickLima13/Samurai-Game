using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausedPanel;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOver;

    private void Update()
    {
        PauseGame();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        audioSource.clip = null;
        audioSource.PlayOneShot(gameOver);
        

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
        AudioListener.pause = true;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        AudioListener.pause = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0;
            pausedPanel.SetActive(true);
            AudioListener.pause = true;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausedPanel.SetActive(false);
        AudioListener.pause = false;

    }
}
