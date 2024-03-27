using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTransform : MonoBehaviour
{
    public LockOnTarget LockOnTarget;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(LockOnTarget.Target != null)
        {
            transform.position = LockOnTarget.Target.transform.position;
        }
    }
}
