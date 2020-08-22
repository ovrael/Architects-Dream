using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    //Dostepne w UNITY
    //UNITY
    [Header("Music volume")]
    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] TMP_Text musicVolumeText;
    [SerializeField] Slider musicSlider;

    [Header("Sounds volume")]
    [SerializeField] AudioMixerGroup soundsMixer;
    [SerializeField] TMP_Text soundsVolumeText;
    [SerializeField] Slider soundsSlider;

    [Header("Player volume")]
    [SerializeField] AudioMixerGroup playerMixer;
    [SerializeField] TMP_Text playerVolumeText;
    [SerializeField] Slider playerSlider;

    [Header("Resolution ")]
    [SerializeField] TMP_Dropdown resolutionDropdown;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI savedGameText;

    [Header("Canvas")]
    [SerializeField] GameObject settingsCanva;

    //C#

    //Prywatne
    //UNITY
    LoadingManager loadingManager;
    Resolution[] resolutions;
    public static MenuScript instance;
    //C#

    private void Awake()
    {
        ResumeGame();
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
    }

    private void Start()
    {
        loadingManager = GameObject.Find("LoadingManager").GetComponent<LoadingManager>();

        #region Getting resolutions

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        #endregion

        #region Setting audio

        float musicVol, soundsVol, playerVol;

        musicMixer.audioMixer.GetFloat("musicVolume", out float mVol);
        musicVol = (mVol + 50) * 100 / 50;
        musicSlider.value = mVol;
        musicVolumeText.SetText(string.Format("{0:0}", musicVol));

        soundsMixer.audioMixer.GetFloat("soundsVolume", out float sVol);
        soundsVol = (sVol + 40) * 100 / 40;
        soundsVolumeText.SetText(string.Format("{0:0}", soundsVol));
        soundsSlider.value = sVol;

        playerMixer.audioMixer.GetFloat("playerVolume", out float pVol);
        playerVol = (pVol + 25) * 100 / 30;
        playerVolumeText.SetText(string.Format("{0:0}", playerVol));
        playerSlider.value = pVol;

        #endregion

    }

    private void Update()
    {
        //if (settingsCanva.activeSelf && Input.GetButtonDown("Cancel"))
        //{
        //    settingsCanva.SetActive(false);
        //    mainCanva.SetActive(true);
        //}   
    }

    public void SaveGame()
    {
        StartCoroutine("ShowText");
    }

    IEnumerator ShowText()
    {
        Debug.Log("Rozpoczynam Coroutine");
        savedGameText.CrossFadeAlpha(150f, 0.5f, true);
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("Po dwoch sekundach Coroutine");
        savedGameText.CrossFadeAlpha(2f, 0.5f, true);
        yield return new WaitForSecondsRealtime(0.4f);
        Debug.Log("Po 2.4sek Coroutine");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        loadingManager.LoadJustAnimation();
        CursorManager.ChangeCursorToShoot();
    }

    public void ResumeGame()
    {
        CursorManager.ChangeCursorToShoot();
        settingsCanva.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        CursorManager.ChangeCursorToMenu();
        loadingManager.LoadJustAnimation();
        settingsCanva.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume) //Sets volume volue in msuic mixer;
    {
        if(((volume + 50) * 100 / 50) == 0)
            musicMixer.audioMixer.SetFloat("musicVolume", -75);
        else
            musicMixer.audioMixer.SetFloat("musicVolume", volume);

        float vol = (volume + 50) * 100 / 50;
        musicVolumeText.SetText(string.Format("{0:0}", vol));
    }

    public void SetSoundsVolume(float volume) //Sets volume volue in sounds mixer;
    {
        if (((volume + 35) * 100 / 40) == 0)
            soundsMixer.audioMixer.SetFloat("soundsVolume", -75);
        else
            soundsMixer.audioMixer.SetFloat("soundsVolume", volume);

        float vol = (volume + 35) * 100 / 40;
        soundsVolumeText.SetText(string.Format("{0:0}", vol));
    }

    public void SetPlayerVolume(float volume) //Sets volume volue in player mixer;
    {
        if (((volume + 25) * 100 / 20) == 0)
            playerMixer.audioMixer.SetFloat("playerVolume", -75);
        else
            playerMixer.audioMixer.SetFloat("playerVolume", volume);

        float vol = (volume + 25) * 100 / 20;
        playerVolumeText.SetText(string.Format("{0:0}", vol));
    }
}