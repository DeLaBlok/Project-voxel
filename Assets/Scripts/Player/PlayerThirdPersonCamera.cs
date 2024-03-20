using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdPersonCamera : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    public float RotationSpeed = 8f;
    PlayerCombat PlayerCombat;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CameraDir = Camera.main.transform.forward;
        CameraDir = Vector3.ProjectOnPlane(CameraDir, Vector3.up);

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        if(_verticalInput != 0 && PlayerCombat.DirMustReset == false|| _horizontalInput != 0 && PlayerCombat.DirMustReset == false)
        {
            transform.forward = Vector3.Slerp(transform.forward, CameraDir, Time.deltaTime * RotationSpeed);
        }
        
    }
}
