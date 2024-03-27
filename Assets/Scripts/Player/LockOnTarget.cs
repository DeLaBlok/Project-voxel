using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    public GameObject Target = null;
    public GameObject LockTarget = null;
    private bool _oldTarget = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var Position = transform.position;
        float Distance = float.PositiveInfinity;
        foreach(var Enemy in GameManager.Instance.EnemiesInLockOnRange)
        {
            var d = (Enemy.transform.position - Position).sqrMagnitude;
            if(d < Distance)
            {
                Target = Enemy;
                Distance = d;
            }
        }

        if(GameManager.Instance.EnemiesInLockOnRange.Count <= 0)
        {
            Distance = float.PositiveInfinity;
            Target = null;
        }

    }

    private void KeepTarget()
    {
        if(_oldTarget == false)
        {
            LockTarget = Target;
        }
    }
}
