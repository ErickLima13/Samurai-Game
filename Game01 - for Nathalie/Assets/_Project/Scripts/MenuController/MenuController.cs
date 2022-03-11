using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Audio;


public class MenuController : MonoBehaviour
{
    public AudioMixer mixer;

    public GameObject controlsPanel;
    
    public void Play()
    {
        StartCoroutine(ShowControls());
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    IEnumerator ShowControls()
    {
        controlsPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void SetVolumeBGM(float volume)
    {
        mixer.SetFloat("BGM", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        mixer.SetFloat("SFX", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
