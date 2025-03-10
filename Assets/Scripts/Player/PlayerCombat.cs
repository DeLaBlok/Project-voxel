using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator Animator;
    PlayerStamina PlayerStamina;

    public AudioSource Wazaah;

    private bool _lightInput = false;
    private bool _heavyInput = false;
    private bool _canQueue = false;
    private bool CanStartCombo = true;
    public bool Attacking = false;

    private int _wazaahRNG;

    private float _lightStaminaCost = 20;
    private float _heavyStaminaCost = 30;

    public BoxCollider HitBox;

    public Transform PlayerBody;

    public AudioSource Slash;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        PlayerStamina = GetComponent<PlayerStamina>();
        Physics.IgnoreLayerCollision(7, 10);
    }

    // Update is called once per frame
    void Update()
    {
        QueueNextAttack();
        StartAttack();
        AnimatorSpeed();
    }

    private void StartAttack()
    {
        if(Input.GetMouseButtonDown(0) && CanStartCombo == true && PlayerStamina.Stamina > 0)
        {
            _wazaahRNG = Random.Range(1,501);
            if(_wazaahRNG == 1)
            {
                Wazaah.Play();
            }
            Attacking = true;
            Animator.SetBool("LightInput", true);
            HitBox.tag = "LightAttack";
            CanStartCombo = false;

            PlayerStamina.Stamina -= _lightStaminaCost;
            PlayerStamina.Cooldown();
        }

        if(Input.GetMouseButtonDown(1) && CanStartCombo == true && PlayerStamina.Stamina > 0)
        {
            _wazaahRNG = Random.Range(1,501);
            if(_wazaahRNG == 1)
            {
                Wazaah.Play();
            }
            Attacking = true;
            Animator.SetBool("HeavyInput", true);
            HitBox.tag = "HeavyAttack";
            CanStartCombo = false;

            PlayerStamina.Stamina -= _heavyStaminaCost;
            PlayerStamina.Cooldown();
        }
    }

    private void QueueNextAttack()
    {
        if(Input.GetMouseButtonDown(0) && _canQueue == true && PlayerStamina.Stamina > 0)
        {
            _lightInput = true;
            _heavyInput = false;
        }

        if(Input.GetMouseButtonDown(1) && _canQueue == true && PlayerStamina.Stamina > 0)
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

            PlayerStamina.Stamina -= _lightStaminaCost;
            PlayerStamina.Cooldown();
        }
        else if(_heavyInput == true)
        {
            Animator.SetBool("HeavyInput", true);
            HitBox.tag = "HeavyAttack";

            PlayerStamina.Stamina -= _heavyStaminaCost;
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

    public void ResetHitbox()
    {
        if(HitBox.enabled == true)
        {
            HitBox.enabled = false;
        }
    }

    private void AnimatorSpeed()
    {
        if(Attacking == true)
        {
            Animator.speed = 0.8f;
        }
        else
        {
            Animator.speed = 1;
        }
    }

    public void PlayAttackSound()
    {
        Slash.Play();
    }
}
