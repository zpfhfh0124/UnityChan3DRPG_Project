using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //public string SceneToLoad;
    private AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
    }

    public void LoadGame()
    {
        audio.Stop();
        SceneManager.LoadScene("CityScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
