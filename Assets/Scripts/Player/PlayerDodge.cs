using UnityEngine;
using System.Collections;

public class PlayerDodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    [SerializeField] private float dodgeDistance = 3.8f;
    [SerializeField] private float dodgeDuration = 0.25f;
    [SerializeField] private float dodgeCooldown = 3f;

    private bool isDodging;
    private float lastDodgeTime;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDodging) return;

        if(Time.time < lastDodgeTime + dodgeCooldown) return;

        if(Input.GetKeyDown(KeyCode.Q)) Dodge(transform.forward);
    }

    private void Dodge(Vector3 direction)
    {
        direction.y = 0f;
        direction.Normalize();

        StartCoroutine(DodgeRoutine(direction));
    }

    private IEnumerator DodgeRoutine(Vector3 direction)
    {
        isDodging = true;

        animator.SetTrigger("Dodging");

        yield return new WaitForSeconds(1.65f);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction * dodgeDistance;

        float elapsed = 0f;

        while (elapsed < dodgeDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / dodgeDuration;

            transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            yield return null;
        }

        transform.position = targetPosition;

        isDodging = false;
    }
}
