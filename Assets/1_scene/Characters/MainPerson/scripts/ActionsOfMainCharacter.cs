using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsOfMainCharacter : MonoBehaviour
{

    private Animator animator;
    private bool fightModVar = false;
    private bool magicModVar = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetBool("Walk", true);
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

    public void TakeOnWeapon()
    {
        animator.SetBool("FightMod", true);
        fightModVar = true;
    }

    public void TakeOutWeapon()
    {
        animator.SetBool("FightMod", false);
        fightModVar = false;
    }


    public bool getFightMod()
    {
        return fightModVar;
    }

    public bool getMagicMod()
    {
        return magicModVar;
    }

    public void makeAttackTriger()
    {
        animator.SetTrigger("Attack");
    }

    public void makeMagicAttackTriger()
    {
        animator.SetTrigger("MagicAttack");
    }

    public void setSwordMode()
    {
        animator.SetBool("Sword", true);
    }

    public void setOutSwordMode()
    {
        animator.SetBool("Sword", false);
    }

    public void turnOnMagic()
    {
        animator.SetBool("Magic", true);
        magicModVar = true;
    }

    public void turnOffMagic()
    {
        animator.SetBool("Magic", false);
        magicModVar = false;
    }

}
