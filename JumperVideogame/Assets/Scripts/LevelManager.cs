using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void SoundMenu()
    {
        SceneManager.LoadScene("SoundMenu");
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void CityScene()
    {
        SceneManager.LoadScene("CityScene");
    }

    public void UndergroundScene()
    {
        SceneManager.LoadScene("UndergroundScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
