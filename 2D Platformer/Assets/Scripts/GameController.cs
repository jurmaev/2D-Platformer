using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float GameOverDelay;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Text endOfLevel;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject gameWonScreen;
    [SerializeField] private Health bossHealth;

    private void Update()
    {
        if(playerHealth._dead)
            Invoke(nameof(GameOver), GameOverDelay);
        if(bossHealth != null && bossHealth._dead)
            Invoke(nameof(GameWon), GameOverDelay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        levelCompleteUI.SetActive(true);
        foreach(var star in stars)
            star.SetActive(false);
        var starsNumber = (int)(progressBar.fillAmount * 100 / 33);
        if (starsNumber == 0)
        {
            endOfLevel.text = "Уровень не пройден";
            buttons[0].SetActive(false);
        }
        else
        {
            endOfLevel.text = "Уровень пройден!";
            buttons[1].SetActive(false);
        }
        for(var i = 0; i < starsNumber; i++)
            stars[i].SetActive(true);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void GameWon()
    {
        gameWonScreen.SetActive(true);
    }

    public void GameOver()
    {
        _gameOverScreen.Setup();
    }
}
