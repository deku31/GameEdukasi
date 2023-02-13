using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    public int highscore;

    public void save()
    {
        PlayerPrefs.SetInt("Input",highscore);
        Debug.Log("Save");
    }
    public void Load()
    {
        Debug.Log("Load");
        PlayerPrefs.GetInt("Input", highscore);
        highscore = PlayerPrefs.GetInt("Input");
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
