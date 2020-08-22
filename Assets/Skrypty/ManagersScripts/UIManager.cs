using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject mainCanva;

    public static UIManager instance;

    LoadingManager loadingManager;

    bool justOpenSettings;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        loadingManager = GameObject.Find("LoadingManager").GetComponent<LoadingManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (!mainCanva.activeSelf)
            {
                CursorManager.ChangeCursorToMenu();
                Time.timeScale = 0f;
                mainCanva.SetActive(true);
                StartCoroutine("CanClose");
            }
            if (mainCanva.activeSelf && !justOpenSettings)
            {
                ResumeGame();
            }
        }
    }

    IEnumerator CanClose()
    {
        justOpenSettings = true;

        yield return new WaitForSecondsRealtime(0.2f);

        justOpenSettings = false;
    }

    public void ResumeGame()
    {
        CursorManager.ChangeCursorToShoot();
        mainCanva.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        CursorManager.ChangeCursorToMenu();
        loadingManager.LoadJustAnimation();
        mainCanva.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
