using System;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(10, 12);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Health>().Heal(healthValue);
            gameObject.SetActive(false);
        }

    }
}
