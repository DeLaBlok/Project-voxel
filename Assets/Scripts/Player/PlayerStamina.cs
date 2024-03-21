using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private float _maxStamina = 100;
    public float Stamina;
    private float _staminaRegenAmount = 7;

    private bool _cooldown = false;
    public float CooldownTimer = 2;
    // Start is called before the first frame update
    void Start()
    {
        Stamina = _maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        StaminaRegen();
        StaminaCap();
    }

    private void StaminaRegen()
    {
        if(_cooldown == false && Stamina != _maxStamina)
        {
            Stamina += _staminaRegenAmount * Time.deltaTime;
        }
    }

    private void StaminaCap()
    {
        if(Stamina > _maxStamina)
        {
            Stamina = _maxStamina;
        }
    }

    public void Cooldown()
    {
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        _cooldown = true;
        Debug.Log("Start");
        yield return new WaitForSeconds(CooldownTimer);
        _cooldown = false;
        Debug.Log("End");
    }
}
