using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;

    [Header("Animation")]
    public Animator anim;

    [YarnCommand("ChangeBG")]
    public void ChangeBG(int index)
    {
        anim.SetTrigger("Play");
        image.sprite = sprites[index];        
    }

    [YarnCommand("ChangeScene")]
    public void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }
}
