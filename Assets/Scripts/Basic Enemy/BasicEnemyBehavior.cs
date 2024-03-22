using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    Animator Animator;
    Rigidbody Rigidbody;

    private int _choice = 0;
    private bool _choiceGenerated = false;
    private bool _isHurt = false;
    private float _health = 100;

    public float MoveSpeed = 6;
    public float LightDamage = 10;
    public float HeavyDamage = 20;

    public DetectPlayer DetectPlayer;
    public AttackRange AttackRange;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(7, 8);
    }

    // Update is called once per frame
    void Update()
    {
        MakeChoice();
        RNGChoice();
        LookAtPlayer();
        SpeedControl();
        Death();
    }

    private void FixedUpdate()
    {
        WalkToPlayer();
    }

    private void MakeChoice()
    {
        switch(_choice)
        {
            case 1:
                Animator.SetBool("LightAttack", true);
                break;
            case 2:
                Animator.SetBool("HeavyAttack", true);
                break;
            default:

                break;
        }
    }

    private void RNGChoice()
    {
        if(AttackRange.PlayerInRange == true && _choiceGenerated == false)
        {
            _choice = Random.Range(1,3);
            _choiceGenerated = true;
        }
    }

    public void ResetAttack()
    {
        _choice = 0;
        _choiceGenerated = false;
        Animator.SetBool("LightAttack", false);
        Animator.SetBool("HeavyAttack", false);
    }

    private void WalkToPlayer()
    {
        if(DetectPlayer.PlayerDetected == true && _choice == 0 && _isHurt == false)
        {
            Animator.SetBool("Walking", true);
            Rigidbody.AddRelativeForce(Vector3.forward * MoveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            Animator.SetBool("Walking", false);
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

    private void LookAtPlayer()
    {
        if(DetectPlayer.PlayerDetected == true && _choice == 0 && _isHurt == false)
        {
            transform.LookAt(Player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LightAttack"))
        {
            _health -= LightDamage;
            Animator.SetBool("Hurt", true);
            _isHurt = true;
        }
        else if(other.CompareTag("HeavyAttack"))
        {
            _health -= HeavyDamage;
            Animator.SetBool("Hurt", true);
            _isHurt = true;
        }
    }

    public void ResetHurt()
    {
        _isHurt = false;
        Animator.SetBool("Hurt", false);
    }

    private void Death()
    {
        if(_health <= 0)
        {
            Animator.SetBool("Dead", true);
        }
    }

    public void AnimatorSetDead()
    {
        Animator.SetBool("HasDied", true);
    }
}
