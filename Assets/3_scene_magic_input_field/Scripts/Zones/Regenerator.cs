using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerator : MonoBehaviour
{

    private PassiveCustomAbility regeniratorAbility = null;

    // Start is called before the first frame update
    void Start()
    {
        regeniratorAbility = new PassiveCustomAbility("Regineration");

        AbilityFloatComponent hCompanent = new AbilityFloatComponent("H", AbilitySettings.ComponentValueTarget.Health);
        hCompanent.value = 3f;
        hCompanent.supportRexpansion = false;
        regeniratorAbility.AddComponent(hCompanent);

        hCompanent = new AbilityFloatComponent("A", AbilitySettings.ComponentValueTarget.Armor);
        hCompanent.value = 5f;
        hCompanent.supportRexpansion = false;
        regeniratorAbility.AddComponent(hCompanent);

        hCompanent = new AbilityFloatComponent("M", AbilitySettings.ComponentValueTarget.Mana);
        hCompanent.value = 10f;
        hCompanent.supportRexpansion = false;
        regeniratorAbility.AddComponent(hCompanent);

        // Time Component

        AbilityTimeComponent timeComponent = new AbilityTimeComponent("T");
        timeComponent.time = 1f;
        timeComponent.timeType = AbilitySettings.AbilityTimedType.Repeat;
        regeniratorAbility.AddComponent(timeComponent);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.AddAbility(this.regeniratorAbility);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        SpellManager objectManager = other.gameObject.SpellManager();
        if (objectManager)
        {
            objectManager.RemoveAbility(this.regeniratorAbility);
        }
    }


}
