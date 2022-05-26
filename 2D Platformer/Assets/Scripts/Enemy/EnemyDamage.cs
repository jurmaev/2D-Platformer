using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float _damage;

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            col.GetComponent<Health>().TakeDamage(_damage);
    }
}
