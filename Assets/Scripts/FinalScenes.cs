using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FinalScenes : MonoBehaviour
{
    public bool theEnd;
    public VideoPlayer videoStart;
    public VideoPlayer videoBadEnd;
    public VideoPlayer videoGoodEnd;

    private void Start()
    {
        if (videoStart != null) videoStart.loopPointReached += OnStartVideoEnd;
        if (videoBadEnd != null) videoBadEnd.loopPointReached += OnBadVideoEnd;
        if (videoGoodEnd != null) videoGoodEnd.loopPointReached += OnGoodVideoEnd;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!theEnd) SceneManager.LoadScene("BadEndVideo");
            if (theEnd) SceneManager.LoadScene("GoodEndVideo");
        }
    }
    void OnStartVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Видео завершено!");
        SceneManager.LoadScene("Lobby");
    }
    void OnBadVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu");

    }

    void OnGoodVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu");
    }
}
