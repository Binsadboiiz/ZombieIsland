using UnityEngine;
using UnityEngine.InputSystem;

//quản lý máu và xử lý sát thương của zombie
public class ZombieHealth : MonoBehaviour, IDamageable
{
    //lượng máu tối đa của zombie
    [SerializeField] private float maxHealth = 100f;

    //lượng máu hiện tại của zombie
    private float currentHealth;

    private ZombieDeath zombieDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        zombieDeath = GetComponent<ZombieDeath>();
    }

    //xử lý khi zombie nhận sát thương
    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
        {
            return;
        }

        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0f);

        if (currentHealth <= 0)
        {
            zombieDeath.Die();
        }
        Debug.Log(
            $"Zombie took {damage} damage. " +
            $"HP: {currentHealth}/{maxHealth}"
        );
    }

    //test nhận sát thương
    private void Update()
    {
        //nhấn space để test nhận sát thương
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
    }
}