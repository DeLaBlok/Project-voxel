using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody Rigidbody;
    Animator Animator;
    public float MoveSpeed = 8f;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _canMove = true;
    private bool _lockedOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void CanMoveToggle()
    {
        _canMove = !_canMove;
    }

    private void PlayerInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        AnimatorController();  
    }

    private void Movement()
    {
        if(_canMove == true)
        {
            Vector3 Moving = new Vector3 (_horizontalInput * MoveSpeed, 0f, _verticalInput * MoveSpeed);
            Rigidbody.AddRelativeForce(Moving.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
    }   

    private void SpeedControl()
    {
        Vector3 FlatVel = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
        if(FlatVel.magnitude > MoveSpeed)
        {
            Vector3 LimitedVel = FlatVel.normalized * MoveSpeed;
            Rigidbody.velocity = new Vector3(LimitedVel.x, Rigidbody.velocity.y, LimitedVel.z);
        }
    }   

    private void AnimatorController()
    {
        Animator.SetFloat("X", _horizontalInput);
        Animator.SetFloat("Y", _verticalInput);
    }
}
