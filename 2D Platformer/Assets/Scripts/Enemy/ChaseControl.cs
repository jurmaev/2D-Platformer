using System;
using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    [SerializeField] private FlyingEnemy enemy;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(12, 11);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        enemy.chase = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        enemy.chase = false;
    }
}