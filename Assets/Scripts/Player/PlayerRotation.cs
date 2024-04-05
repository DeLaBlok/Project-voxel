using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private float _rotationSpeed = 10;

    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        Rotate();
    }

    private void PlayerInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void Rotate()
    {
        Vector3 Rotation = new Vector3 (_horizontalInput, 0, _verticalInput);
        if(Rotation != Vector3.zero && PlayerMovement.CanMove == true && PlayerMovement.Dodge == false)
        {
            Quaternion ToRotation = Quaternion.LookRotation(Rotation, Vector3.up);

            transform.localRotation = ToRotation;
        }
    }
}
