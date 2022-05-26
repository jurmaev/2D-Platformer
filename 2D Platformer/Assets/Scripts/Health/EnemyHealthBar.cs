using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Image healthBar;

    void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, enemyHealth.CurrentHealth / enemyHealth.GetMaxHealth(),
            lerpSpeed * Time.deltaTime);
        ChangeColor();
    }

    private void ChangeColor()
    {
        var healthColor = Color.Lerp(Color.red, Color.green, enemyHealth.CurrentHealth / enemyHealth.GetMaxHealth());
        healthBar.color = healthColor;
    }
}
