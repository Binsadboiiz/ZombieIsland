using UnityEngine;

public class AimSystem : MonoBehaviour
{
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxAimDistance = 50f;
    [SerializeField] private LayerMask aimLayerMask;
    private Vector3 aimPoint;

    public Vector3 AimPoint => aimPoint;

    private void Update()
    {
        UpdateAimPoint();
    }

    private void UpdateAimPoint()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        Ray ray = playerCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, maxAimDistance, aimLayerMask))
        {
            aimPoint = hit.point;
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue);
        } else
        {
            aimPoint = ray.origin + ray.direction * maxAimDistance;
            Debug.DrawRay(ray.origin, ray.direction * maxAimDistance, Color.blue);
        }
    }
}
