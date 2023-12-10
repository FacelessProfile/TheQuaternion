using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLast : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ButtonArthur()
    {
        SceneManager.LoadScene("GoodEndVideo");
    }
    public void ButtonNOArthur()
    {
        SceneManager.LoadScene("BadEndVideo");
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
