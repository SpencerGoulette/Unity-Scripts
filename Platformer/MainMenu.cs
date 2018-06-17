using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Play button is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Testing");
    }

    // Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
