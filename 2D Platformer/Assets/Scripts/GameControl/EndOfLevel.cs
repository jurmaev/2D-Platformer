using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private void OnTriggerEnter2D(Collider2D col)
    {
        gameController.CompleteLevel();
    }
}
