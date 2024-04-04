using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    Animator Animator;
    Rigidbody Rigidbody;

    public BoxCollider HitBox;

    private int _choice = 0;
    private float _health = 100;

    private bool _choiceGenerated = false;
    private bool _isHurt = false;
    private bool _cooldown = false;
    private bool _attacking = false;

    public float MoveSpeed = 7;
    public float LightDamage = 10;
    public float HeavyDamage = 20;

    public DetectPlayer DetectPlayer;
    public AttackRange AttackRange;
    public AudioSource Pain;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Enemies.Add(this.gameObject);

        _player = GameObject.FindGameObjectWithTag("PlayerTarget");

        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        Physics.IgnoreLayerCollision(7, 8);
        Physics.IgnoreLayerCollision(7, 10);
    }

    // Update is called once per frame
    void Update()
    {
        MakeChoice();
        RNGChoice();
        LookAtPlayer();
        SpeedControl();
        Death();
        AnimatorSpeed();
    }

    private void FixedUpdate()
    {
        WalkToPlayer();
    }

    private void MakeChoice()
    {
        switch(_choice)
        {
            case 0:
                break;
            case 1:
                Animator.SetBool("LightAttack", true);
                HitBox.tag = "EnemyLightAttack";
                _attacking = true;
                break;
            case 2:
                Animator.SetBool("HeavyAttack", true);
                HitBox.tag = "EnemyHeavyAttack";
                _attacking = true;
                break;
            default:

                break;
        }
    }

    private void RNGChoice()
    {
        if(AttackRange.PlayerInRange == true && _choiceGenerated == false && _cooldown == false)
        {
            _choice = Random.Range(1,3);
            _choiceGenerated = true;
        }
    }

    public void ResetAttack()
    {
        _cooldown = false;
        _choiceGenerated = false;
    }

    public void ToggleHitbox()
    {
        HitBox.enabled = ! HitBox.enabled;
    }

    public void ResetHitbox()
    {
        if(HitBox.enabled == true)
        {
            HitBox.enabled = false;
        }
    }

    private void WalkToPlayer()
    {
        if(DetectPlayer.PlayerDetected == true && _choice == 0 && _isHurt == false && AttackRange.PlayerInRange == false)
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
            transform.LookAt(_player.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LightAttack"))
        {
            _health -= LightDamage;
            Animator.SetTrigger("PlayHurt");
            _isHurt = true;
        }
        else if(other.CompareTag("HeavyAttack"))
        {
            _health -= HeavyDamage;
            Animator.SetTrigger("PlayHurt");
            _isHurt = true;
        }
    }

    public void ResetHurt()
    {
        _isHurt = false;
        Animator.ResetTrigger("PlayHurt");
    }

    private void Death()
    {
        if(_health <= 0)
        {
            Animator.SetBool("Dead", true);
            GameManager.Instance.Enemies.Remove(this.gameObject);
            GameManager.Instance.EnemiesInLockOnRange.Remove(this.gameObject);
            StartCoroutine(RemoveObject());
        }
    }

    public void AnimatorSetDead()
    {
        Animator.SetBool("HasDied", true);
    }

    IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }

    public void SetCooldown()
    {
        _attacking = false;
        _choice = 0;
        Animator.SetBool("LightAttack", false);
        Animator.SetBool("HeavyAttack", false);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        _cooldown = true;
        yield return new WaitForSeconds(2);
        ResetAttack();
    }

    public void PlayPain()
    {
        Pain.Play();
    }

    public void AnimatorSpeed()
    {
        if(_attacking == true)
        {
            Animator.speed = 0.8f;
        }
        else
        {
            Animator.speed = 1;
        }
    }
}
