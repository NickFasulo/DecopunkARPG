using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Aiming Settings")]
    [SerializeField] private LayerMask groundLayer;

    private CharacterController controller;
    private Camera mainCamera;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        HandleAiming();
    }

    private void HandleMovement()
    {
        // Read WASD / Arrow Key inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Convert movement input relative to isometric camera (45-degree angle)
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Rotate direction 45 degrees to match isometric perspective
            Vector3 moveDirection = Quaternion.Euler(0, 45, 0) * inputDirection;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleAiming()
    {
        // Cast a ray from camera through screen mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            // Find point on ground where mouse is pointing
            Vector3 targetPoint = hitInfo.point;
            targetPoint.y = transform.position.y; // Keep character level

            // Look toward the mouse position
            Vector3 lookDirection = targetPoint - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}