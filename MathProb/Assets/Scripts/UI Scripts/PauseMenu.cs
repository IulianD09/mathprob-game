﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{ 
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject options;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)            
                Resume();
            else
                Pause();
            if (options.gameObject.activeSelf)
            {
                OptionsMenu();
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                    options.SetActive(false);
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void MenuLoad()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void OptionsMenu()
    {
        Time.timeScale = 0f;
        options.SetActive(true);
    }
}
