using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private GameObject[] enemies;
    private float allEnemies;
    private float aliveEnemies;

    [SerializeField] private float lerpSpeed;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        allEnemies = enemies.Length;
        aliveEnemies = allEnemies;
        progressBar.fillAmount = (allEnemies - aliveEnemies) / allEnemies;
    }

    private void Update()
    {
        var count = 0;
        foreach (var enemy in enemies)
            if (enemy.activeSelf) count++;
        aliveEnemies = count;
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, (allEnemies - aliveEnemies) / allEnemies,
            lerpSpeed * Time.deltaTime);
    }
}