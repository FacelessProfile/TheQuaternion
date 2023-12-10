using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public string[] lines;
    public float speedText;
    public Text dialogText;
    public Text dialogText2;
    public GameObject Choice;
    public GameObject MainScene;

    private int index;

    void Start()
    {
        index = 0;
        StartDialog();
    }

    void StartDialog()
    {
        dialogText.text = string.Empty;
        dialogText2.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (index % 2 == 0)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                dialogText.text += c;
                yield return new WaitForSeconds(speedText);
            }

            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            foreach (char c in lines[index].ToCharArray())
            {
                dialogText2.text += c;
                yield return new WaitForSeconds(speedText);
            }

            yield return new WaitForSeconds(1.0f);
        }

        NextLine();
    }

    public void SkipTextClick()
    {
        StopAllCoroutines();
        dialogText.text = lines[index];
        NextLine();
    }

    void NextLine()
    {
        index++;

        if (index >= lines.Length)
        {
            index = 0;
            HandleLastLineEvent();
        }
        else
        {
            StartDialog();
        }
    }

    void HandleLastLineEvent()
    {
        dialogText.text = string.Empty;
        dialogText2.text = string.Empty;
        MainScene.SetActive(false);
        Choice.SetActive(true);
    }
}
