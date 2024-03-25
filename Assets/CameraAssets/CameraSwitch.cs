using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.LockedOn == true)
        {
            Animator.SetBool("LockOn", true);
        }
        else
        {
            Animator.SetBool("LockOn", false);
        }
    }
}
