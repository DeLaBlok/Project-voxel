using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHitEffect : MonoBehaviour
{
    public GameObject HitEffect;

    public Transform HitEffectPoint;

    public void PlayEffect()
    {
        Instantiate(HitEffect,HitEffectPoint);
    }
}
