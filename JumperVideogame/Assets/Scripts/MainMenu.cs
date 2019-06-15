using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Quando carrega "Start", passa para a "cena" da cidade (início do jogo)
    public void StartGame()
    {
        SceneManager.LoadScene("CityScene");
    }

    // Quado carrega "Quit", sai-se do jogo
    public void QuitGame()
    {
        // De modo a saber, no editor, que se saiu do jogo
        Debug.Log("Saíste o jogo");

        Application.Quit();
    }

}
