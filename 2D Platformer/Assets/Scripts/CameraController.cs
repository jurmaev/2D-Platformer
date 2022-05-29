using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset; 
    [SerializeField] private float cameraSpeed;
    private float _lookAhead;

    private void FixedUpdate()
    {
        var targetPosition = player.position + cameraOffset;
        var smoothPosition = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), cameraSpeed * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
