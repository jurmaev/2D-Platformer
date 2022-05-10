using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")] [SerializeField] private float maxHealth;
    public float CurrentHealth { get; private set; }
    private Animator _anim;
    public bool _dead { get; private set; }

    [Header("iFrames")] [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer _spriteRenderer;

    [Header("Components")] [SerializeField]
    private Behaviour[] components;


    private void Awake()
    {
        CurrentHealth = maxHealth;
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth > 0)
        {
            _anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else if (!_dead)
        {
            Die();
        }
    }

    public void Die()
    {
        _anim.SetTrigger("die");
            
        foreach (var component in components)
            component.enabled = false;

        _dead = true;
    }
    
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void Heal(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, maxHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (var i = 0; i < numberOfFlashes; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0, .5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Deactivate()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }
}