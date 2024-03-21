using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Rigidbody Rigidbody;
    PlayerCombat PlayerCombat;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PlayerCombat = GetComponent<PlayerCombat>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 CameraDir = Camera.main.transform.forward;
        CameraDir = Vector3.ProjectOnPlane(CameraDir, Vector3.up);
        if(PlayerCombat.Attacking == false)
        {
            Rigidbody.rotation = Quaternion.FromToRotation(Vector3.forward, CameraDir);
        }  
    }
}
