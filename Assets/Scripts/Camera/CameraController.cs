using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

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

    private void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;

        // Initialize the rotation values based on the current camera rotation
        Vector3 currentRotation = transform.eulerAngles;

        rotationX = currentRotation.x;
        rotationY = currentRotation.y;
    }

    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }
        HandleMouseRotation();
        FollowTarget();
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
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
        Vector3 targetPosition = target.position + Vector3.up * height;

        transform.position = targetPosition + offset;
        transform.rotation = rotation;
    }
}
