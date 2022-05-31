using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Image progressBar;
    private GameObject[] _enemies;
    private float _allEnemies;
    private float _aliveEnemies;
    
    private void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _allEnemies = _enemies.Length;
        _aliveEnemies = _allEnemies;
        progressBar.fillAmount = (_allEnemies - _aliveEnemies) / _allEnemies;
    }

    private void Update()
    {
        var count = 0;
        foreach (var enemy in _enemies)
            if (enemy.activeSelf) count++;
        _aliveEnemies = count;
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, (_allEnemies - _aliveEnemies) / _allEnemies,
            lerpSpeed * Time.deltaTime);
    }
}