using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Zombie died");
        Destroy(gameObject);
    }
}
