using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int LevelCount;

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
}
