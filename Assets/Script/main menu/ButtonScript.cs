using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public SoundManager sfx;
    string _scene;
    private void Start()
    {
        sfx = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            keluar();
        }
    }
    public void navigasi(string SceneTujuan)
    {
        _scene = SceneTujuan;
        sfx._sfx(0);
        StartCoroutine(ChangeScene(0.8f));

    }
    
    public void keluar()
    {
        Application.Quit();
    }
    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(_scene);
        StopAllCoroutines();
    }
}
