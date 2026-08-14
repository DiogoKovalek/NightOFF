using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControler : MonoBehaviour {
    //[Header("Buttons")]
    //[Header("ShiftCounter")]
    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseMenu;


    public void OnPauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Pause");
    }
    public void OnContinueGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Continue");
    }
    public void OnRestartGame() {
        Debug.Log("Restart");
        ManagerScenes.RestartLevel();
    }
    public void OnExitGame() {
        Debug.Log("Exit");
        ManagerScenes.ExitToHomeScreen();
    }
}
