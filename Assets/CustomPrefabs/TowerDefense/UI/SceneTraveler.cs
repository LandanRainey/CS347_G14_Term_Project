using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEditor; 

public class SceneTraveler : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void gameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quit()
    {
        Application.Quit(); 
    }
}
