using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _startHealth = 100;
    private float _health;

    private int _flaskAmount = 3;
    public float HealAmount = 50;

    PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _health = _startHealth;
        PlayerMovement = GetComponent<PlayerMovement>();
        Physics.IgnoreLayerCollision(9, 10);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();
        PlayerHealing();
        HealthCap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyLightAttack") && PlayerMovement.Dodge == false)
        {
            _health -= 10;
        }

        if(other.CompareTag("EnemyHeavyAttack") && PlayerMovement.Dodge == false)
        {
            _health -= 20;
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
}
