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
        animator.SetBool("Walk", true);
    }

    

    public void Stay()
    {
        animator.SetBool("Walk", false);
    }

    

}
