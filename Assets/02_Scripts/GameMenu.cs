﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject[] anyWindow = new GameObject[4];
    void Start()
    {
        foreach (GameObject window in anyWindow)
        {
            window.SetActive(false);
        }
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
        if (anyWindow[3].activeSelf)
        {
            //當背包是開的 把他關起來
            anyWindow[3].SetActive(false);
        }
        else if (!anyWindow[0].activeSelf)
        {
            //如果主選單是關閉的
            //for迴圈關閉所有視窗
            foreach (GameObject window in anyWindow)
            {
                window.SetActive(false);
            }
            //再把主選單打開
            anyWindow[0].SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {   
            //主選單是開著的就把它關掉
            anyWindow[0].SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void GameContinue()
    {
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0) ;
        //聽說build出來真的會結束但是在unity裡面不會有任何作用所以用debug.log來代替
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
