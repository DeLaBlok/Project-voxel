using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float _startHealth = 100;
    private float _health;

    private int _flaskAmount = 3;
    public float HealAmount = 50;

    public bool Hurting = false;

    PlayerMovement PlayerMovement;
    Animator Animator;

    public Image HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        _health = _startHealth;
        PlayerMovement = GetComponent<PlayerMovement>();
        Animator = GetComponent<Animator>();
        Physics.IgnoreLayerCollision(9, 10);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();
        PlayerHealing();
        HealthCap();
        HealthBarFill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyLightAttack") && PlayerMovement.Dodge == false)
        {
            _health -= 10;
            Animator.SetTrigger("Hurt");
            Hurting = true;
        }

        if(other.CompareTag("EnemyHeavyAttack") && PlayerMovement.Dodge == false)
        {
            _health -= 20;
            Animator.SetTrigger("Hurt");
            Hurting = true;
        }
    }

    private void PlayerHealing()
    {
        if(Input.GetKeyDown("q") && _flaskAmount != 0)
        {
            _flaskAmount -= 1;
            _health += HealAmount;
        }
    }

    private void HealthCap()
    {
        if(_health > _startHealth)
        {
            _health = _startHealth;
        }
    }

    private void PlayerDeath()
    {
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void HealthBarFill()
    {
        HealthBar.fillAmount = _health / _startHealth;
    }

    public void ResetHurt()
    {
        Hurting = false;
        Animator.ResetTrigger("Hurt");
    }
}
