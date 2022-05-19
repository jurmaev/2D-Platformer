using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float _damage;

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log("collision");
    //     if(col.gameObject.CompareTag("Player"))
    //         col.gameObject.GetComponent<Health>().TakeDamage(_damage);
    // }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            col.GetComponent<Health>().TakeDamage(_damage);
    }
}
