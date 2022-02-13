using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtElement : MonoBehaviour
{
    private PassiveCustomAbility hurtAbility = null;


    private void Start()
    {
        this.hurtAbility = new PassiveCustomAbility("Hurt");

        AbilityFloatComponent hurtComponent = new AbilityFloatComponent("H", AbilitySettings.ComponentValueTarget.Health);
        hurtComponent.value = -50f;
        hurtComponent.supportRexpansion = false;
        this.hurtAbility.AddComponent(hurtComponent);

        hurtComponent = new AbilityFloatComponent("A", AbilitySettings.ComponentValueTarget.Armor);
        hurtComponent.value = -100f;
        hurtComponent.supportRexpansion = false;
        this.hurtAbility.AddComponent(hurtComponent);
    }




    public void OnTriggerEnter(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.AddAbility(this.hurtAbility);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.RemoveAbility(this.hurtAbility);
        }
    }
}
