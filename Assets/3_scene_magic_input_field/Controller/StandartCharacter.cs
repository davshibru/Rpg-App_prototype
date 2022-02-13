using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartCharacter : AbilityBihaviour
{
    [Range(0f, 100f)]
    public float health = 100f;
    [Range(0f, 200f)]
    public float armor = 200f;
    [Range(0f, 300f)]
    public float mana = 250;
    [Range(100f, 500f)]
    public float moveSpeed = 250f;

    private void Start()
    {
        /*
        PassiveCustomAbility ability = new PassiveCustomAbility("Move Speed");
        AbilityFloatComponent component = new AbilityFloatComponent("Speed", AbilitySettings.ComponentValueTarget.MoveSpeed);
        component.value = 100;
        ability.AddComponent(component);
        yield return new WaitForSeconds(1f);
        this.spellManager.AddAbility(ability);
        yield return new WaitForSeconds(5f);
        this.spellManager.RemoveAbility(ability);
        */
    }

    public override void OnComponentExpansion(AbilityComponent component)
    {
        /*
        AbilitySettings.ComponentValueTarget valueTarget = component.targetValue;
        object value = component.objectValue;
        Debug.Log("Got component: " + valueTarget + " with type: " + value.GetType() + " in order to add.");
        switch (valueTarget)
        {
            case AbilitySettings.ComponentValueTarget.MoveSpeed:
                try { this.moveSpeed += (float)value; } catch { throw new System.FieldAccessException(); }
                break;
            default:
                return;
                break;
        }
        Debug.Log("Character move speed: " + this.moveSpeed);
        */
         
        switch (component.targetValue)
        {
            case AbilitySettings.ComponentValueTarget.Special:
                break;
            case AbilitySettings.ComponentValueTarget.Health:
                try
                {
                    this.health = Mathf.Clamp(this.health + (float)component.objectValue, 0, 100);
                }
                catch
                {

                    throw new System.ArgumentException();
                }
                break;
            case AbilitySettings.ComponentValueTarget.Mana:
                try
                {
                    this.mana = Mathf.Clamp(this.armor + (float)component.objectValue, 0, 300);
                }
                catch
                {

                    throw new System.ArgumentException();
                }
                break;
            case AbilitySettings.ComponentValueTarget.Armor:
                try
                {
                    this.armor = Mathf.Clamp(this.armor + (float)component.objectValue, 0, 200);
                }
                catch
                {

                    throw new System.ArgumentException();
                }

                break;
            case AbilitySettings.ComponentValueTarget.Time:
                break;
            case AbilitySettings.ComponentValueTarget.Invis:
                break;
            case AbilitySettings.ComponentValueTarget.MoveSpeed:
                break;
            default:
                break;
        }
    }



    public override void OnComponentRexpansion(AbilityComponent component)
    {
        AbilitySettings.ComponentValueTarget valueTarget = component.targetValue;
        object value = component.objectValue;
        Debug.Log("Got component: " + valueTarget + " with type: " + value.GetType() + " in order to remove.");
        switch (valueTarget)
        {
            case AbilitySettings.ComponentValueTarget.MoveSpeed:
                try { this.moveSpeed -= (float)value; } catch { throw new System.FieldAccessException(); }
                break;
            default:
                return;
                break;
        }
        Debug.Log("Character move speed: " + this.moveSpeed);
    }
}
