using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    private void OnTriggerEnter2D(Collider2D col)
    {
        _gameController.CompleteLevel();
    }
}
