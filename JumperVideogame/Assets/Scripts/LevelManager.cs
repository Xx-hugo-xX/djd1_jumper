using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene currentScene;

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScreen")
        {
            if (Input.GetKeyDown("space"))
            {
                MainMenu();
            }
        }

    }




    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CityScene()
    {
        SceneManager.LoadScene("CityScene");
    }

    public void UndergroundScene()
    {
        SceneManager.LoadScene("UndergroundScene");
    }


    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
