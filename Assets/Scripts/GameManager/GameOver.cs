using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverWhite, gameOverBlack;


    public void GameOverWhite()
    {
        //play killanimation for white
        gameOverWhite.SetActive(true);
    }

    public void GameOverBlack() 
    { 
        //play killanimation for black
        gameOverBlack.SetActive(true);
    }

}
