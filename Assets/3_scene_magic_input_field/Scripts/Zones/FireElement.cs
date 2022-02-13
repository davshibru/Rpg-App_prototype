using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : MonoBehaviour
{

    private PassiveCustomAbility fireAbillity = null;

    private void Start()
    {
        this.fireAbillity = new PassiveCustomAbility("Fire");


        AbilityFloatComponent fireComponent = new AbilityFloatComponent("F", AbilitySettings.ComponentValueTarget.Health);
        fireComponent.value = -5.5f;
        fireComponent.supportRexpansion = false;
        this.fireAbillity.AddComponent(fireComponent);


        // Time Component

        AbilityTimeComponent timeComponent = new AbilityTimeComponent("T");
        timeComponent.time = 1f;
        timeComponent.timeType = AbilitySettings.AbilityTimedType.Repeat;
        fireAbillity.AddComponent(timeComponent);
    }


    public void OnTriggerEnter(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.AddAbility(this.fireAbillity);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.RemoveAbility(this.fireAbillity);
        }
    }
}
