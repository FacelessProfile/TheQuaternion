using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public string LoadData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            Debug.LogWarning("Key not found: " + key);
            return null;
        }
    }
}
