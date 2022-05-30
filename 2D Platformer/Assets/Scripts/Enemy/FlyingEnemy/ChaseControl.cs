using System;
using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    [SerializeField] private FlyingEnemy[] enemies;

    // private void Awake()
    // {
    //     Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        foreach (var enemy in enemies)
            enemy.chase = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (var enemy in enemies)
            enemy.chase = false;
    }
}