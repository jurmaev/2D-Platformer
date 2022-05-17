using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;

    public void GameOver()
    {
        _gameOverScreen.Setup();
    }
}
