using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float _startHealth = 100;
    private float _health;

    private int _flaskAmount = 4;
    private float _flaskFill;
    public float HealAmount = 50;

    public bool Hurting = false;

    PlayerMovement PlayerMovement;
    Animator Animator;

    public Image HealthBar;
    public Image Flask;

    public GameObject HealEffect;
    public Transform EffectPoint;

    public AudioSource HealSound;
    public AudioSource HurtSound;
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
        FlaskFill();
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
            Animator.SetTrigger("Healing"); 
        }
    }

    public void HealingEffect()
    {
        _flaskAmount -= 1;
        _health += HealAmount;
        Instantiate(HealEffect,EffectPoint);
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
            GameManager.Instance.ResetGameManager();
            Destroy(gameObject);
            SceneManager.LoadScene("LoseScreen");
        }
    }

    private void HealthBarFill()
    {
        HealthBar.fillAmount = _health / _startHealth;
    }

    private void FlaskFill()
    {
        switch(_flaskAmount)
        {
            case 4:
                _flaskFill = 1;
                break;
            case 3:
                _flaskFill = 0.75f;
                break;
            case 2:
                _flaskFill = 0.5f;
                break;
            case 1:
                _flaskFill = 0.25f;
                break;
            case 0:
                _flaskFill = 0;
                break;
            default:
                _flaskFill = 0;
                break;
        }

        Flask.fillAmount = _flaskFill;
    }

    public void PlayHurtSound()
    {
        HurtSound.Play();
    }

    public void PlayHealSound()
    {
        HealSound.Play();
    }

    public void ResetHealing()
    {
        Animator.ResetTrigger("Healing");
    }

    public void ResetHurt()
    {
        Hurting = false;
        Animator.ResetTrigger("Hurt");
    }
}
