using UnityEngine;

//đối tượng có interface này có khả năng nhận damage
public interface IDamageable
{
    //xử lý damage nhận vào
    void TakeDamage(float damage);
}