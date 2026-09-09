using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class ManagerScenes {
    private static string[] scenesLevel = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" };
    private static string sceneHomeScreen = "HomeScreen";
    private static string miniGameScreen = "Questionary";
    private static int numLevel = 1; // 0 -> nao esta em nivel
    private static bool inQuestionary = false;

    public static void NextLevel() {
        Time.timeScale = 1f;
        if (!inQuestionary && numLevel != scenesLevel.Length) {
            inQuestionary = true;
            SceneManager.LoadScene(miniGameScreen);
        }
        else {
            inQuestionary = false;
            numLevel++;
            if (scenesLevel.Length < numLevel) ExitToHomeScreen();
            else SceneManager.LoadScene(scenesLevel[numLevel - 1]);
        }
    }
    public static void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scenesLevel[numLevel - 1]);
    }
    public static void ExitToHomeScreen() {
        Time.timeScale = 1f;
        numLevel = 0;
        //Questionario
        ListQuestions.ClearList();
        
        SceneManager.LoadScene(sceneHomeScreen);
    }
    public static void StartGame() {
        Time.timeScale = 1f;
        numLevel = 1;
        //Questionario
        ListQuestions.ClearList();

        SceneManager.LoadScene(scenesLevel[numLevel - 1]);
    }
}
