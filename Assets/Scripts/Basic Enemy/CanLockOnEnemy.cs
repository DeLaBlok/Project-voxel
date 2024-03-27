using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanLockOnEnemy : MonoBehaviour
{
    private void Start()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LockOnRange"))
        {
            GameManager.Instance.EnemiesInLockOnRange.Add(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("LockOnRange"))
        {
            GameManager.Instance.EnemiesInLockOnRange.Remove(this.gameObject);
        }
    }
}
