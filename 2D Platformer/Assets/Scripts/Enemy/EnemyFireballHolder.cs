using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        transform.localScale *= Mathf.Sign(enemy.localScale.x);
    }
}
