using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player's Transform in the Inspector
    public Vector3 offset; // Offset between the camera and the player
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
{
    if (player != null)
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.2f);
    }
}
}
