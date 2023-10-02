using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform tracked;

    [SerializeField]
    private float rotationSensitivity = 30;

    [SerializeField]
    private Text uiText;

    private float xRot = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        uiText.text = rotationSensitivity.ToString();
    }

    public void GatherDelta(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RotateCamera(context.ReadValue<Vector2>());
        }
    }

    public void GatherScrollWheel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotationSensitivity = Mathf.Clamp(rotationSensitivity + context.ReadValue<Vector2>().y * .1f, 1f, 180f);
            uiText.text = rotationSensitivity.ToString();
        }
    }

    private void RotateCamera(Vector2 delta)
    {
        tracked.Rotate(rotationSensitivity * delta.x * Time.deltaTime * Vector3.up);

        xRot += rotationSensitivity * -delta.y * Time.deltaTime;

        if (xRot < 90f && xRot > -90f)
            transform.RotateAround(transform.position, transform.right, rotationSensitivity * -delta.y * Time.deltaTime);
        else
        {
            xRot = Mathf.Clamp(xRot, -90f, 90f);
            transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        
    }
}
