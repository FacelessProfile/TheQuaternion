using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Renderer objectRenderer;
    private SaveManager saveManager;
    public string Progress; 

    public int levelCount;

    void Awake()
    {
        
    }

    public void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        saveManager = GetComponent<SaveManager>();
        if (SceneManager.GetActiveScene().name == "Lobby") Progress = saveManager.LoadData($"Level_{levelCount}");
        Debug.LogWarning(Progress);
    }

    public void LevelLoad(int LevelCount)
    {
        if (levelCount <= 1) {
            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                SceneManager.LoadScene($"Level_{LevelCount}");
            }
            else {
                SceneManager.LoadScene($"Level_{LevelCount++}");
            }
        }

        else {
            SceneManager.LoadScene("Lobby");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelLoad(levelCount);
        }
    }
    void Update()
    {
        if (Progress == "Passed")
        {
            objectRenderer.material.color = new Color(255f, 255f, 1f);
        }
        else
        {
            objectRenderer.material.color = new Color(0f, 0f, 0f);
        }
    }
}
