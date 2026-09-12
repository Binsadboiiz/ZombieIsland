using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    //tốc độ di chuyển
    [SerializeField] private float movementSpeed = 1f;

    //vị trí cần di chuyển tới
    private Vector3 targetPosition;

    //kiểm tra zombie có target không
    private bool hasTarget;

    //đặt vị trí cần di chuyển tới
    public void MoveTo(Vector3 newTargetPosition)
    {
        hasTarget = true;
        targetPosition = newTargetPosition;
    }

    public void StopMove()
    {
        hasTarget = false;
    }

    private void Update()
    {
        if (hasTarget)
        {
            //di chuyển zombie về phía vị trí mục tiêu
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                movementSpeed * Time.deltaTime
            );
        } 
    }
}