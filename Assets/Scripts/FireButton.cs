using UnityEngine;

public class FireButton : MonoBehaviour
{
    public PlayerController playerController;
    public float reloadTime;
    private float readyToShoot;

    private void Update()
    {
#if UNITY_EDITOR
        UseMouseInput();
#elif UNITY_ANDROID
        UseTouchScreenInput();
#endif
    }

    private void UseTouchScreenInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.transform.gameObject.name == "FireButton")
                        {
                            Shooting();
                            break;
                        }
                    }
                }
            }
        }
    }

    private void UseMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.gameObject.name == "FireButton")
                {
                    Shooting();
                }
            }
        }
    }
    private void Shooting()
    {
        if (readyToShoot < Time.time)
        {
            readyToShoot = Time.time + reloadTime;
            playerController.isShooting = true;
        }
    }
}
