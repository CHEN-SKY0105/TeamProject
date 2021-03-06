﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject[] anyWindow = new GameObject[3];
    public GameObject welcomeImage;
    void Start()
    {
        Time.timeScale = 1f;
        //一開始先把所有視窗打開讓參數讀的到遊戲開始時自動關閉
        foreach (GameObject window in anyWindow)
        {
            window.SetActive(false);
        }
        anyWindow[0].SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscButton();
        }
    }
    public void EscButton()
    {
        if (!anyWindow[0].activeSelf)
        {
            //for所有的視窗SetActive(false)
            foreach (GameObject window in anyWindow)
            {
                window.SetActive(false);
            }

            anyWindow[0].SetActive(true);
        }
    }
    public void QuitGame()
    {
        //聽說build出來真的會結束但是在unity裡面不會有任何作用所以用debug.log來代替
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void WelcomeOff()
    {
        welcomeImage.SetActive(false);
    }
}
