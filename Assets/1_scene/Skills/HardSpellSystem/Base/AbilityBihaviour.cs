using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class AbilityBihaviour : MonoBehaviour
{

    private SpellManager objectManager = null;

    public SpellManager spellManager
    {
        get
        {
            if (this.objectManager == null)
            {
                this.objectManager = this.gameObject.GetComponent<SpellManager>();
                if (this.objectManager == null) this.objectManager = this.gameObject.AddComponent<SpellManager>();
            }
            return this.objectManager;
        }
    }

    public virtual void OnAbilityAdded(AbilityObject ability)
    {
        Debug.Log("Character: " + this.name + " has got new ability: " + ability.name);
    }

    public virtual void OnAbilityUsed(AbilityObject ability)
    {
        Debug.Log("Character: " + this.name + " used ability: " + ability.name);
    }

    public virtual void OnAbilityRemoved(AbilityObject ability)
    {
        Debug.Log("Character: " + this.name + " has removed ability: " + ability.name);
    }

    public virtual void OnAbilityEndUse(AbilityObject ability)
    {
        Debug.Log("Character: " + this.name + " stopped to use ability: " + ability.name);
    }

    public abstract void OnComponentExpansion(AbilityComponent component);                  
    public abstract void OnComponentRexpansion(AbilityComponent component);
}



#region Data

// link - http://unity3d.ru/distribution/viewtopic.php?f=11&t=29750&p=196985#p196985

public static class SpellManagerData
{

    public static SpellManager SpellManager(this GameObject gameObject)
    {
        return gameObject.GetComponent<SpellManager>();
    }

    public enum AbilitySendEvent
    {
        OnAbilityAdded,
        OnAbilityUsed,
        OnAbilityEndUse,
        OnAbilityRemoved
    }

    public enum ComponentSendEvent
    {
        OnComponentExpansion,
        OnComponentRexpansion
    }


}

public sealed class AbilitiesManagerData
{
    private readonly Dictionary<AbilitySettings.AbilityType, List<AbilityData>> abilities;

    public bool AbilityExists(AbilityObject ability)
    {
        return this.abilities[ability.type].Any(d => d.ability == ability);
    }

    public AbilityData FindAbilityData(AbilityObject ability)
    {
        return this.abilities[ability.type].FirstOrDefault(d => d.ability == ability);
    }

    public bool AbilityExists(string name)
    {
        return FindAbility(name) != null;
    }

    public AbilityObject FindAbility(string name)
    {
        AbilityData data = this.abilities[AbilitySettings.AbilityType.Active].FirstOrDefault(d => d.ability.name == name);
        if (data == null)
            data = this.abilities[AbilitySettings.AbilityType.Passive].FirstOrDefault(d => d.ability.name == name);
        return data.ability;
    }

    public void AddAbilityData(AbilityData data)
    {
        if (this.abilities[data.ability.type].Contains(data) == false)
        {
            this.abilities[data.ability.type].Add(data);
        }
    }

    public void RemoveAbility(string name)
    {
        this.RemoveAbility(FindAbility(name));
    }

    public void RemoveAbility(AbilityObject ability)
    {
        this.RemoveAbilityData(this.abilities[ability.type].FirstOrDefault(d => d.ability == ability));
    }

    public void RemoveAbilityData(AbilityData data)
    {
        for (int index = 0; index < this.abilities[data.ability.type].Count; index++)
        {
            if (this.abilities[data.ability.type][index] == data)
            {
                this.abilities[data.ability.type].RemoveAt(index);
                return;
            }
        }
    }

    public IEnumerable<ActiveAbilityData> activeAbilities
    {
        get
        {
            foreach (AbilityData data in this.abilities[AbilitySettings.AbilityType.Active])
                yield return data as ActiveAbilityData;
        }
    }

    public IEnumerable<PassiveAbilityData> passiveAbilities
    {
        get
        {
            foreach (AbilityData data in this.abilities[AbilitySettings.AbilityType.Passive])
                yield return data as PassiveAbilityData;
        }
    }

    public AbilitiesManagerData()
    {
        this.abilities = new Dictionary<AbilitySettings.AbilityType, List<AbilityData>>();
        this.abilities.Add(AbilitySettings.AbilityType.Active, new List<AbilityData>());
        this.abilities.Add(AbilitySettings.AbilityType.Passive, new List<AbilityData>());
    }
}

#endregion


#region AbilityData

public class AbilityData
{

    public readonly AbilityObject ability = null;

    public bool isUsing = false;                        //  булевую переменную, которая давала бы менеджеру понять что эта способность уже инициализировалась и ее нельзя еще повторно инициализировать.

    protected AbilityData(AbilityObject ability)
    {
        this.ability = ability;
    }

}

#endregion

#region два типа класса - обертки для каждого типа способности.


public sealed class PassiveAbilityData : AbilityData
{

    public PassiveAbilityData(PassiveAbility ability) : base(ability) { }

    public new PassiveAbility ability
    {
        get { return base.ability as PassiveAbility; }
    }

}

public sealed class ActiveAbilityData : AbilityData
{

    public ActiveAbilityData(ActiveAbility ability) : base(ability) { }

    public new ActiveAbility ability
    {
        get { return base.ability as ActiveAbility; }
    }

}

#endregion