using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    private float _maxStamina = 100;
    public float Stamina;
    private float _staminaRegenAmount = 7;

    private bool _cooldown = false;
    public float CooldownTimer = 2;

    public Image StaminaBar;
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
        StaminaBarFill();
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
        yield return new WaitForSeconds(CooldownTimer);
        _cooldown = false;
    }

    private void StaminaBarFill()
    {
        StaminaBar.fillAmount = Stamina / _maxStamina;
    }
}
