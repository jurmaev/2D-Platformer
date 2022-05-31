using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    public bool Dead { get; private set; }


    [Header("Health")] [SerializeField] private float maxHealth;
    private Animator _anim;

    [Header("Components")] [SerializeField]
    private Behaviour[] components;


    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth > 0)
            _anim.SetTrigger("hurt");
        else if (!Dead)
        {
            Die();
        }
    }

    public void Die()
    {
        _anim.SetTrigger("die");
        if (gameObject.CompareTag("Enemy"))
            FindObjectOfType<PlayerAttack>().playerMana.RestoreMana(maxHealth / 2.5f);
        foreach (var component in components)
            component.enabled = false;
        Dead = true;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void Heal(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, maxHealth);
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        _anim = GetComponent<Animator>();
    }


    private void Deactivate()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }
}