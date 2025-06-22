using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        FindFirstObjectByType<AudioManager>().Play("Play");
        SceneManager.LoadScene(1);
    }

    public void RestartLevel()
    {
        FindFirstObjectByType<AudioManager>().Play("Select");
        SceneManager.LoadScene(2);
    }

    public void Menu()
    {
        FindFirstObjectByType<AudioManager>().Play("Select");
        SceneManager.LoadScene(0);
    }
}
