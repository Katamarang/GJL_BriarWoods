using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] PopUps;
    public GameObject Tutorials;
    int popUpIndex = 0;

    private void Update()
    {
        if (popUpIndex == PopUps.Length)
        {
            Tutorials.SetActive(false);
            enabled = false;
        }
        for (int i = 0; i < PopUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                PopUps[i].SetActive(true);
            } else
            {
                PopUps[i].SetActive(false);
            }
        }
    }

    public void ContinueTutorial()
    {       
        popUpIndex++; 
        FindFirstObjectByType<AudioManager>().Play("PopLow");
        //print("Next Lesson" + popUpIndex);
    }
    public void SkipTutorial()
    {
        popUpIndex = PopUps.Length;
    }
}
