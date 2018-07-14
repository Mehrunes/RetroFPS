using RetroFPS.Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Transform actorEye;
    public Transform hud;
    public Transform menuPause;

    public bool lockCursor = true;

    void Awake()
    {
        menuPause.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            lockCursor = !lockCursor;
        }
        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (menuPause.gameObject.activeInHierarchy == false)
        {
            menuPause.gameObject.SetActive(true);
            Time.timeScale = 0;
            actorEye.gameObject.SetActive(false);
            hud.gameObject.SetActive(false);
        }
        else
        {
            menuPause.gameObject.SetActive(false);
            Time.timeScale = 1;
            actorEye.gameObject.SetActive(true);
            hud.gameObject.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        menuPause.gameObject.SetActive(false);
        Time.timeScale = 1;
        actorEye.gameObject.SetActive(true);
        hud.gameObject.SetActive(true);
        lockCursor = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void SaveAsExit()
    {
        //Na razie powrot do scenyny menu główne
        SceneManager.LoadScene("Menu glowne");
        SceneManager.UnloadScene("Scena testowa 1");
    }
}
