using UnityEngine;

//xử lý hành động tấn công của zombie
public class ZombieAttack : MonoBehaviour
{
    //lượng damage zombie gây ra
    [SerializeField] private float attackDamage;

    //đối tượng sẽ tấn công
    private Transform targetAttack;

    //thời gian chờ giữa hai lần tấn công
    [SerializeField] private float attackCooldown;
    private float attackTimer;

    //đặt đối tượng cần tấn công
    public void SetTarget(Transform target)
    {
        targetAttack = target;
    }

    public void Attack()
    {
        //ngăn những lần gọi Attack() trong khoảng thời gian cooldown sau đòn đánh
        if (attackTimer > 0)
        {
            return;
        }

        //tham chiếu đến mục tiêu nhận damage thông qua interface
        IDamageable damageable = targetAttack.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(attackDamage);

            attackTimer = attackCooldown;
        }
    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }
}