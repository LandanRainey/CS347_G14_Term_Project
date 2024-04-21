using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEditor; 

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI; 
    public GameObject gameWinUI;
    public TowerDefense round; 

    public GameObject tower1;
    public GameObject tower2;

    public void gameWin()
    {
        if(round.currentRound == round.maxRounds)
        {
            gameWinUI.SetActive(true);
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true); 
    }

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

    ///// UPGRADE & SELL BUTTON TRIAL //////

    /*public GameObject other; 
    public void hellIfIKnow()
    {
        replace(tower1,tower2); 
    }

    public void replace(GameObject obj1, GameObject obj2)
    {
        Instantiate(obj2, obj1.transform.position, Quaternion.identity);
        DestroyImmediate(other,true);
    }*/

}
