using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    
    private PlayableDirector fadingTimeline;
    public Slider volumeSlider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        fadingTimeline = GetComponentInChildren<PlayableDirector>();
        fadingTimeline.gameObject.SetActive(false);

        volumeSlider.value = PlayerPrefs.GetFloat("_volume");
    }

    public void Pause(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void LoadSceneWrapper(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    public void LoadSceneQuick(int index)
    {
        SceneManager.LoadScene(index);
    }

    private IEnumerator LoadScene(int index)
    {
        Time.timeScale = 1;
        fadingTimeline.gameObject.SetActive(true);
        fadingTimeline.Play();

        while (true)
        {
            yield return new WaitUntil(timlineFinished);
            break;
        }

        SceneManager.LoadScene(index);
        yield return null;
    }

    private bool timlineFinished()
    {
        return fadingTimeline.time > fadingTimeline.duration - 0.2f;
    }

    public void Exit()
   {
       Application.Quit();
   }

    public void SetVolumePref()
    {
        PlayerPrefs.SetFloat("_volume", volumeSlider.value);
    }
}
