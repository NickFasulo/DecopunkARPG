using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target; // Drag your Player object here

    [Header("Camera Offset & Smoothing")]
    [SerializeField] private Vector3 offset = new Vector3(-10f, 15f, -10f);
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired position based on player location + offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera toward the desired position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}