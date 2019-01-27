using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScneneTransition : MonoBehaviour
{
    public Animator anim;
    private int levelToLoadl;
    public Image transitionImage;
    //public bool transition = false;
    
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoadl = levelIndex;
        anim.SetTrigger("end");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoadl);
    }
    public void OnDisapear()
    {
        transitionImage.enabled = false;
    }
    public void OnAppear()
    {
        transitionImage.enabled = true;
    }
}
