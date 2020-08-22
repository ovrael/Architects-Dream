using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CursorManager : MonoBehaviour
{
    [Header("Cursors")]
    [SerializeField] Texture2D menuCursorTexture;
    [SerializeField] Texture2D inGameCursorTexture;
    static Texture2D menuCursor;
    static Texture2D inGameCursor;

    public static CursorManager instance;

    private void Awake()
    {
        menuCursor = menuCursorTexture;
        inGameCursor = inGameCursorTexture;

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "MainMenu")
            ChangeCursorToMenu();
        else
            ChangeCursorToShoot();


        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ChangeCursorToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeCursorToMenu()
    {
        Cursor.SetCursor(menuCursor, Vector2.zero, CursorMode.Auto);
    }

    public static void ChangeCursorToShoot()
    {
        float xOffset = 7.5f;
        float yOffset = 7.5f;
        Cursor.SetCursor(inGameCursor, new Vector2(xOffset, yOffset), CursorMode.Auto);
    }
}
