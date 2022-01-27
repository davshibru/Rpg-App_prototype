using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsOfMainCharacter : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetFloat("Speed", 0.5f);
    }

    public void Run()
    {
        animator.SetFloat("Speed", 1f);
    }

    public void Stay()
    {
        animator.SetBool("Walk", false);
        animator.SetFloat("Speed", 0f);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }



}
