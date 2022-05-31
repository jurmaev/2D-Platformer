using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float gameOverDelay;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Text endOfLevel;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject gameWonScreen;
    [SerializeField] private Health bossHealth;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        levelCompleteUI.SetActive(true);
        foreach (var star in stars)
            star.SetActive(false);
        var starsNumber = (int) (progressBar.fillAmount * 100 / 33);
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

        for (var i = 0; i < starsNumber; i++)
            stars[i].SetActive(true);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        gameOverScreen.Setup();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (playerHealth.Dead)
            Invoke(nameof(GameOver), 5);
        if (bossHealth != null && bossHealth.Dead)
            Invoke(nameof(GameWon), gameOverDelay);
    }


    private void GameWon()
    {
        gameWonScreen.SetActive(true);
    }
}