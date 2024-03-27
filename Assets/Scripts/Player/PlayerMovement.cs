using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody Rigidbody;
    Animator Animator;
    LockOnTarget LockOnTarget;

    public float MoveSpeed = 8;
    public float DodgeSpeed = 5;
    private float _horizontalInput;
    private float _verticalInput;

    public bool CanMove = true;
    public bool Dodge = false;
    public bool LockedOn = false;
    private bool _canLockOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        LockOnTarget = GetComponent<LockOnTarget>();
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
        Dodging();
    }

    public void CanMoveToggle()
    {
        CanMove = !CanMove;
    }

    private void PlayerInput()
    {
        if(Dodge == false)
        {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        }

        if(Input.GetKeyDown("space") && Dodge == false)
        {
            Dodge = true;
        }

        AnimatorController();  

        if(Input.GetKeyDown(KeyCode.LeftShift) && LockedOn == false && GameManager.Instance.EnemiesInLockOnRange.Count != 0)
        {
            LockedOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift) && LockedOn == true || GameManager.Instance.EnemiesInLockOnRange.Count <= 0)
        {
            LockedOn = false;
        }
    }

    private void Movement()
    {
        if(CanMove == true && Dodge == false)
        {
            Vector3 Moving = new Vector3 (_horizontalInput * MoveSpeed, 0, _verticalInput * MoveSpeed);
            Rigidbody.AddRelativeForce(Moving.normalized * MoveSpeed * 10, ForceMode.Force);
        }
    }   

    private void SpeedControl()
    {
        Vector3 FlatVel = new Vector3(Rigidbody.velocity.x, 0, Rigidbody.velocity.z);
        if(FlatVel.magnitude > MoveSpeed)
        {
            Vector3 LimitedVel = FlatVel.normalized * MoveSpeed;
            Rigidbody.velocity = new Vector3(LimitedVel.x, Rigidbody.velocity.y, LimitedVel.z);
        }
    }   

    private void AnimatorController()
    {
        if(_horizontalInput != 0 && CanMove == true || _verticalInput != 0 && CanMove == true)
        {
            Animator.SetBool("Walking", true);
        }
        else
        {
            Animator.SetBool("Walking", false);
        }
    }

    private void Dodging()
    {
        if(Dodge == true)
        {
            Animator.SetBool("Dodge", true);
            Vector3 Dodge = new Vector3 (_horizontalInput * DodgeSpeed, 0, _verticalInput * DodgeSpeed);
            Rigidbody.AddRelativeForce(Dodge.normalized * DodgeSpeed, ForceMode.Impulse);
        }
    }

    public void ResetDodge()
    {
        Dodge = false;
        Animator.SetBool("Dodge", false);
    }
}
