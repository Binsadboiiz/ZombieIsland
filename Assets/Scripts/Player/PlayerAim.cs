using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    private bool isAiming;
    public bool IsAiming => isAiming;

    // Update is called once per frame
    void Update()
    {
        HandleAim();
    }

    private void HandleAim()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            crosshair.SetActive(true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            crosshair.SetActive(false);
        }
    }
}
