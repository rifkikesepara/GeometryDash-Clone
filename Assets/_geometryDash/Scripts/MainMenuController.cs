using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{ 
    private PlayableDirector fadingTimeline;

    private void Start()
    {
        fadingTimeline = GetComponentInChildren<PlayableDirector>();
        fadingTimeline.gameObject.SetActive(false);
    }

    public void LoadSceneWrapper(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    public IEnumerator LoadScene(int index)
    {
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
}
