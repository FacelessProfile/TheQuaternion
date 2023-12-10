using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public bool somekindofUI = false;
    public GameObject mainMenu;
    public GameObject authors;

    void Start()
    {
        if (somekindofUI) AuthorsOpen();
    }
    public void NewGame()
    {
        SceneManager.LoadScene("StartVideo");
        somekindofUI = true;
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("popal");
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    void AuthorsOpen()
    {
        mainMenu.SetActive(false);
        authors.SetActive(true);
    }
}
