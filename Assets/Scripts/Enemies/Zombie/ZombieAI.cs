using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionRange;
    private Transform targetTransform;
    private ZombieMovement zombieMovement;

    //biến lưu trạng thái debug, tránh spam log (bool mặc định lưu false)
    private bool wasPlayerDetected;

    private void Awake()
    {
        zombieMovement = gameObject.GetComponent<ZombieMovement>();
    }

    private void DetectPlayer()
    {
        //vị trí hiện tại của zombie
        Vector3 zombiePosition = transform.position;

        //xác định vị trí của player
        Vector3 playerPosition = playerTransform.position;

        //tính khoảng cách từ Zombie đến player
        float distance = Vector3.Distance(zombiePosition, playerPosition);

        //xác định trạng thái hiện tại
        bool isPlayerDetected = distance <= detectionRange;

        //log tạm theo dõi trạng thái
        if (isPlayerDetected != wasPlayerDetected)
        {
            if (isPlayerDetected)
            {
                Debug.Log("player detected");
            }
            else
            {
                Debug.Log("player not detected");
            }
        }

        //cập nhật trạng thái cho lần kiểm tra tiếp theo
        wasPlayerDetected = isPlayerDetected;

        if (isPlayerDetected)
        {
            targetTransform = playerTransform;
            zombieMovement.MoveTo(targetTransform.position);
        }
        else
        {
            targetTransform = null;
            zombieMovement.StopMove();
        }
    }

    private void Update()
    {
        DetectPlayer();
    }

}
