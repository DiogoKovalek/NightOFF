using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class ManagerScenes
{
    private static string[] scenesLevel = {"TesteScene"};
    private static string sceneHomeScreen = "HomeScreen";
    private static int numLevel = 1; // 0 -> nao esta em nivel

    public static void NextLevel() {
        Time.timeScale = 1f;
        numLevel++;
        if(scenesLevel.Length < numLevel) numLevel = 1;
        SceneManager.LoadScene(scenesLevel[numLevel]);
    }
    public static void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scenesLevel[numLevel]);
    }
    public static void ExitToHomeScreen() {
        Time.timeScale = 1f;
        numLevel = 0;
        SceneManager.LoadScene(sceneHomeScreen);
    }
    public static void StartGame() {
        Time.timeScale = 1f;
        numLevel = 1;
        SceneManager.LoadScene(scenesLevel[numLevel]);
    }
}
