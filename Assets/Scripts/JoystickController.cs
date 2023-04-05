using System;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Transform manipulator;
    public PlayerController playerController;
    private Vector3 center;
    private Vector3 moveVector;
    private int moveTouchId = -1;

    private void Start()
    {
        center = manipulator.position;
    }

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
                if (moveTouchId < 0)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.transform.gameObject.name == "Joystick")
                            {
                                moveTouchId = touch.fingerId;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (touch.fingerId == moveTouchId)
                    {
                        MoveJoystick(touch.position);
                        if (touch.phase == TouchPhase.Ended)
                        {
                            manipulator.position = center;
                            moveTouchId = -1;
                        }
                        break;
                    }
                }
            }
        }
        //else
        //{
        //    moveTouchId = -1;
        //}
    }

    private void UseMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.gameObject.name == "Joystick")
                {
                    moveTouchId = 1;
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && moveTouchId > 0)
        {
            moveTouchId = -1;
            manipulator.position = center;
        }
        if (moveTouchId > 0)
        {
            MoveJoystick(Input.mousePosition);
        }
    }

    private void MoveJoystick(Vector3 vector)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(vector) - center;
        newPos.z = 0;
        if (Math.Abs(newPos.x) > 0.75 || Math.Abs(newPos.y) > 0.75) newPos = newPos.normalized;
        manipulator.position = center;
        manipulator.Translate(newPos * 0.75f);
    }
}
