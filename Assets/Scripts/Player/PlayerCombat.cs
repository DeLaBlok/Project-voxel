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

    public Transform PlayerBody;

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
            HitBox.tag = "LightAttack";
            CanStartCombo = false;

            PlayerStamina.Stamina -= 10;
            PlayerStamina.Cooldown();
        }

        if(Input.GetMouseButtonDown(1) && CanStartCombo == true)
        {
            Attacking = true;
            Animator.SetBool("HeavyInput", true);
            HitBox.tag = "HeavyAttack";
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
            HitBox.tag = "LightAttack";

            PlayerStamina.Stamina -= 10;
            PlayerStamina.Cooldown();
        }
        else if(_heavyInput == true)
        {
            Animator.SetBool("HeavyInput", true);
            HitBox.tag = "HeavyAttack";

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

    }
}
