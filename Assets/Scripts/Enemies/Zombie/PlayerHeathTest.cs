using UnityEngine;

//xử lý health của player và nhận damage
public class PlayerHeathTest : MonoBehaviour, IDamageable
{
    //lượng health tối đa của player
    [SerializeField] private float maxHealth = 100f;

    //lượng health hiện tại của player
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    //nhận damage từ đối tượng tấn công
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log($"player health: {currentHealth}");
    }
}