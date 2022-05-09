using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private GameObject[] enemies;

    private float allEnemies;

    private float aliveEnemies;

    [SerializeField] private float lerpSpeed;

    [SerializeField] private Image progressBar;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        allEnemies = enemies.Length;
        aliveEnemies = allEnemies;
        progressBar.fillAmount = (allEnemies - aliveEnemies) / allEnemies;
        Debug.Log(allEnemies);
    }

    void Update()
    {
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, (allEnemies - aliveEnemies) / allEnemies,
            lerpSpeed * Time.deltaTime);
    }
}