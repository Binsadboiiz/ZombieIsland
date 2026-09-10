using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    //tốc độ di chuyển
    [SerializeField] private float movementSpeed = 1f;

    //điểm cần di chuyển tới
    [SerializeField] private Transform targetTransform;

    //vị trí cần di chuyển tới
    private Vector3 targetPosition;

    //đặt vị trí cần di chuyển tới
    public void MoveTo(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }

    private void Start()
    {
        MoveTo(targetTransform.position);
    }

    private void Update()
    {
        //di chuyển zombie về phía vị trí mục tiêu
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            movementSpeed * Time.deltaTime
        );
    }

    
}