using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#region interface IAbilityComponentable

/// <summary>
/// components - это массив всех компонентов что содержит способность
/// AddComponent - метод который будет добавлять компоненты.
/// RemoveComponents - метод который будет стирать все компоненты определенного типа AbilityComponent.
/// RemoveComponent - метод который просто будет стирать не нужный пользователю компонент
/// </summary>
public interface IAbilityComponentable
{
    AbilityComponent[] components { get; }
    AbilityComponent AddComponent(AbilityComponent component);
    void RemoveComponents<T>() where T : AbilityComponent;
    void RemoveComponent(AbilityComponent component);
}

#endregion


#region base object ability
public class AbilityObject 
{
    public readonly AbilitySettings.AbilityType type;

    public string name = "";
    public Texture2D icon = null;
    
    public string abilityDescription = "";




    protected AbilityObject(AbilitySettings.AbilityType type, string name)
    {
        this.type = type;
        this.name = name;
        this.abilityDescription = "New " + this.type.ToString() + " ability";

        
    }
}

#endregion


#region passive and active ability

public class PassiveAbility : AbilityObject
{
    public PassiveAbility(string name) : base(AbilitySettings.AbilityType.Passive, name) { }

}

public class ActiveAbility : AbilityObject
{

    public FixedButton castButton = null;

    private float abilityCooldown = 1f;

    public float cooldown
    {
        get { return this.abilityCooldown; }
        set { this.abilityCooldown = (value < 0.1f) ? 0.1f : value; }
    }

    public ActiveAbility(string name) : base(AbilitySettings.AbilityType.Active, name) { }


}

#endregion


#region custom pasive and active ability

/// <summary>
/// Passive
/// </summary>
public class PassiveCustomAbility : PassiveAbility, IAbilityComponentable
{

    private List<AbilityComponent> abilityComponents = new List<AbilityComponent>();
    private AbilityComponent[] abilityArrayComponents;


    public AbilityComponent[] components
    {
        get { return this.abilityArrayComponents; }
    }

    public AbilityComponent AddComponent(AbilityComponent component)
    {
        if (this.abilityComponents.Any(c => c.GetType() == component.GetType()))
        {
            if (component.CheckForAttribute<DisableComponentMultiplyUsage>()) return null;
        }
        this.abilityComponents.Add(component);
        this.abilityArrayComponents = this.abilityComponents.ToArray();
        return component;
    }

    public void RemoveComponent(AbilityComponent component)
    {
        for (int index = 0; index < this.abilityComponents.Count; index++)
        {
            if (this.abilityComponents[index] == component)
            {
                this.abilityComponents.RemoveAt(index);
                this.abilityArrayComponents = this.abilityComponents.ToArray();
                return;
            }
        }
    }

    public void RemoveComponents<T>() where T : AbilityComponent
    {
        foreach (AbilityComponent component in this.abilityArrayComponents.Where(c => c is T))
            RemoveComponent(component);
    }

    public PassiveCustomAbility(string name) : base(name) {
        this.abilityArrayComponents = this.abilityComponents.ToArray();
    }

}


/// <summary>
/// Active
/// </summary>

public class ActiveCustomAbility : ActiveAbility, IAbilityComponentable
{

    private List<AbilityComponent> abilityComponents = new List<AbilityComponent>();
    private AbilityComponent[] abilityArrayComponents;

    

    public AbilityComponent AddComponent(AbilityComponent component)
    {
        if (this.abilityComponents.Any(c => c.GetType() == component.GetType()))
        {
            if (component.CheckForAttribute<DisableComponentMultiplyUsage>()) return null;
        }
        this.abilityComponents.Add(component);
        this.abilityArrayComponents = this.abilityComponents.ToArray();
        return component;
    }

    public void RemoveComponent(AbilityComponent component)
    {
        for (int index = 0; index < this.abilityComponents.Count; index++)
        {
            if (this.abilityComponents[index] == component)
            {
                this.abilityComponents.RemoveAt(index);
                this.abilityArrayComponents = this.abilityComponents.ToArray();
                return;
            }
        }
    }

    public void RemoveComponents<T>() where T : AbilityComponent
    {
        foreach (AbilityComponent component in this.abilityArrayComponents.Where(c => c is T))
            RemoveComponent(component);
    }

    public AbilityComponent[] components
    {
        get { return this.abilityArrayComponents; }
    }

    public ActiveCustomAbility(string name) : base(name) {
        this.abilityArrayComponents = this.abilityComponents.ToArray();
    }

}

#endregion