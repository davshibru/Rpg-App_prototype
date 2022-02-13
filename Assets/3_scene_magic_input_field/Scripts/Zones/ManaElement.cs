using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaElement : MonoBehaviour
{
    private PassiveCustomAbility manaAbility = null;


    private void Start()
    {
        this.manaAbility = new PassiveCustomAbility("Hurt");
        AbilityFloatComponent manaComponent = new AbilityFloatComponent("F", AbilitySettings.ComponentValueTarget.Mana);
        manaComponent.supportRexpansion = false;
        manaComponent.value = -7.5f;
        this.manaAbility.AddComponent(manaComponent);


        // Time Component

        AbilityTimeComponent timeComponent = new AbilityTimeComponent("T");
        timeComponent.time = 1f;
        timeComponent.timeType = AbilitySettings.AbilityTimedType.Repeat;
        manaAbility.AddComponent(timeComponent);
    }




    public void OnTriggerEnter(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.AddAbility(this.manaAbility);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.RemoveAbility(this.manaAbility);
        }
    }
}
