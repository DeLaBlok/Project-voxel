using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator Animator;
    private bool _lightInput = false;
    private bool _heavyInput = false;
    private bool _canQueue = false;
    private bool CanStartCombo = true;
    public bool Attacking = false;

    public BoxCollider HitBox;

    Vector3 AttackDir;
    Vector3 LastForward;
    public Transform PlayerBody;
    private float _horizontal;
    private float _vertical;

    PlayerStamina PlayerStamina;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        PlayerStamina = GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {
        QueueNextAttack();
        StartAttack();
        AttackDirection();
    }

    private void StartAttack()
    {
        if(Input.GetMouseButtonDown(0) && CanStartCombo == true)
        {
            Attacking = true;
            Animator.SetBool("LightInput", true);
            CanStartCombo = false;

            PlayerStamina.Stamina -= 10;
            PlayerStamina.Cooldown();
        }

        if(Input.GetMouseButtonDown(1) && CanStartCombo == true)
        {
            Attacking = true;
            Animator.SetBool("HeavyInput", true);
            CanStartCombo = false;

            PlayerStamina.Stamina -= 20;
            PlayerStamina.Cooldown();
        }
    }

    private void QueueNextAttack()
    {
        if(Input.GetMouseButtonDown(0) && _canQueue == true)
        {
            _lightInput = true;
            _heavyInput = false;
        }

        if(Input.GetMouseButtonDown(1) && _canQueue == true)
        {
            _heavyInput = true;
            _lightInput = false;
        }
    }

    public void CanQueue()
    {
        if(_canQueue == false)
        {
            _canQueue = true;
        }
        else
        {
            _canQueue = false;
        }
    }

    public void NextAttack()
    {
        if(_lightInput == true)
        {
            Animator.SetBool("LightInput", true);

            PlayerStamina.Stamina -= 10;
            PlayerStamina.Cooldown();
        }
        else if(_heavyInput == true)
        {
            Animator.SetBool("HeavyInput", true);

            PlayerStamina.Stamina -= 20;
            PlayerStamina.Cooldown();
        }
        else
        {

        }
    }

    public void ToggleHitbox()
    {
        HitBox.enabled = ! HitBox.enabled;
    }

    public void ResetAttack()
    {
        Animator.SetBool("LightInput", false);
        _lightInput = false;
        Animator.SetBool("HeavyInput", false);
        _heavyInput = false;
    }

    public void ResetCombo()
    {
        CanStartCombo = true;
        Attacking = false;
    }

    private void AttackDirection()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if(_horizontal != 0 || _vertical != 0)
        {
            AttackDir = PlayerBody.transform.forward;
        }

        if(Attacking == true && AttackDir != null)
        {
            transform.forward = AttackDir;
        }
    }

    public void DirectionReset()
    {

    }
}
