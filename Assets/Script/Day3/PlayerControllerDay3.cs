using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDay3 : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        /* Play() 함수 방식이고, 권장되지 않음
        if(Input.GetKeyDown(KeyCode.I))
        {
            animator.Play("Idle");
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            animator.Play("RUN00_F");
        }
        */
        if(Input.GetKeyDown(KeyCode.I))
        {
            animator.SetFloat("moveSpeed", 0.0f);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetFloat("moveSpeed", 5.0f);
        }
    }
}
