using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Orientation;
    public Transform Player;
    public Transform PlayerObj;
    public Rigidbody Rigidbody;

    public float RotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 ViewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = ViewDir.normalized;

        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 InputDir = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        if(InputDir != Vector3.zero)
        {
            PlayerObj.forward = Vector3.Slerp(PlayerObj.forward, InputDir.normalized, Time.deltaTime * RotationSpeed);
        }
    }
}
