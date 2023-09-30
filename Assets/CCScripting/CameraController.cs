using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform tracked;

    [SerializeField]
    private float rotationSensitivity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GatherDelta(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RotateCamera(context.ReadValue<Vector2>());
        }
    }

    public void GatherMoveInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }

        if (context.canceled)
        {

        }
    }

    private void RotateCamera(Vector2 delta)
    {
        tracked.Rotate(rotationSensitivity * delta.x * Time.deltaTime * Vector3.up);
        transform.RotateAround(transform.position, transform.right, rotationSensitivity * -delta.y * Time.deltaTime);
    }
}
