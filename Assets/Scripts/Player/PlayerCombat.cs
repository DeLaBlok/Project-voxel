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

    public BoxCollider HitBox;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        QueueNextAttack();
        StartAttack();
    }

    private void StartAttack()
    {
        if(Input.GetMouseButtonDown(0) && CanStartCombo == true)
        {
            Animator.SetBool("LightInput", true);
            CanStartCombo = false;
        }

        if(Input.GetMouseButtonDown(1) && CanStartCombo == true)
        {
            Animator.SetBool("HeavyInput", true);
            CanStartCombo = false;
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
        }
        else if(_heavyInput == true)
        {
            Animator.SetBool("HeavyInput", true);
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
    }
}
