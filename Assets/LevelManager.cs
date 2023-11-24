using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Renderer objectRenderer;
    private SaveManager saveManager;
    private string Progress; 

    public int LevelCount;

    void Awake()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        saveManager = GetComponent<SaveManager>();
        Progress = saveManager.LoadData($"Level_{LevelCount.ToString()}");
        Debug.LogWarning(Progress);
        if (Progress == "Passed")
        {

            objectRenderer.material.color = new Color(255f, 255f, 1f);
        }
    }

    void Start()
    {

    }

    private void LevelLoad(int LevelCount)
    {
        SceneManager.LoadScene($"Level_{LevelCount}");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelLoad(LevelCount);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            Debug.LogWarning(Progress);
        }
    }
}
