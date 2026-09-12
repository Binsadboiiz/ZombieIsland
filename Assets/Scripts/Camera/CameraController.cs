using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private Transform aimCameraTarget;
    [SerializeField] private float cameraSmoothSpeed = 5f;
    [SerializeField] private Transform player;

    [Header("Aim Settings")]
    [SerializeField] private float aimFOV = 45f;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float fovSmoothSpeed = 10f;

    [Header("Aim Rotation")]
    [SerializeField] private float aimRotationSpeed = 10f;

    [Header("Camera Settings")]
    [SerializeField] private float distance = 2f;
    [SerializeField] private float height = 0.5f;

    [Header("Mouse setting")]
    [SerializeField] private float mouseSensitivity = 2.0f;

    [Header("Vertical Totation")]
    [SerializeField] private float minVerticalAngle = -45f;
    [SerializeField] private float maxVerticalAngle = 45f;

    private float rotationX;
    private float rotationY;
    private Camera cam;

    private void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;

        cam = GetComponent<Camera>();

        // Initialize the rotation values based on the current camera rotation
        Vector3 currentRotation = transform.eulerAngles;

        rotationX = currentRotation.x;
        rotationY = currentRotation.y;
    }

    private void LateUpdate()
    {
        if(cameraTarget == null)
        {
            return;
        }
        HandleMouseRotation();
        FollowTarget();
        HandleFOV();
        HandleAimRotation();
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate 360 degrees around the Y-axis (horizontal rotation)
        rotationY += mouseX;

        // Rotate around the X-axis (vertical rotation) and clamp the angle
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
    }

    private void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        Transform target = cameraTarget;
        if (playerAim != null && playerAim.IsAiming && aimCameraTarget) target = aimCameraTarget;

        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        Vector3 targetPosition = target.position + Vector3.up * height;

        Vector3 desiredPosition = targetPosition + offset;

        if(playerAim != null && playerAim.IsAiming)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime);
        } else
        {
            transform.position = desiredPosition;
        }

        transform.rotation = rotation;
    }

    private void HandleFOV()
    {
        float targetFOV = playerAim.IsAiming ? aimFOV : normalFOV;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovSmoothSpeed * Time.deltaTime);
    }

    private void HandleAimRotation()
    {
        if (playerAim == null || !playerAim.IsAiming) return;

        Quaternion targetRotation = Quaternion.Euler(0f, rotationY, 0f);

        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, aimRotationSpeed * Time.deltaTime);
    }
}
