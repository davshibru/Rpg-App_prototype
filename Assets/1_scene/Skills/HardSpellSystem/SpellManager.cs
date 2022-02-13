using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpellManager : MonoBehaviour
{

    public AbilityBihaviour objectSpellBihaviour { get; private set; }


    private List<TimedComponentData> timedAbilities = new List<TimedComponentData>();


    protected AbilitiesManagerData data { get; private set; }


    protected virtual void Awake()
    {
        this.objectSpellBihaviour = this.GetComponent<AbilityBihaviour>();
        this.data = new AbilitiesManagerData();
    }


    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        /*
        foreach (ActiveAbilityData data in this.data.activeAbilities)
        {
            if (data.ability.castButton.Pressed)
            {
                UseAbility(data);
                return;
            }
        }
        */

        UpdateTimedAbilities();

    }

    

    private void UpdateTimedAbilities()
    {
        for (int index = 0; index < this.timedAbilities.Count; index++)
        {
            TimedComponentData data = this.timedAbilities[index];
            if (Time.time >= data.startTime)
            {
                this.UseAbility(data.data);
                if (data.timeComponent.timeType == AbilitySettings.AbilityTimedType.Once)
                {
                    this.timedAbilities.RemoveAt(index);
                    if (data.data.ability.type == AbilitySettings.AbilityType.Passive)
                        this.RemoveAbility(data.data.ability);
                }
                else if (data.timeComponent.timeType == AbilitySettings.AbilityTimedType.Repeat)
                {
                    data.startTime = Time.time + data.timeComponent.time;
                }
            }
        }
    }

    protected virtual void SpecialComponentExpansion(AbilityComponent component, AbilityData data)
    {
        if (component.targetValue == AbilitySettings.ComponentValueTarget.Time)
        {
            AbilityTimeComponent timeComponent = component as AbilityTimeComponent;
            TimedComponentData timedData = new TimedComponentData(data, timeComponent);
            timedData.startTime = Time.time + timeComponent.time;
            this.timedAbilities.Add(timedData);
        }
    }

    public void AddAbility(AbilityObject ability)
    {
        if (this.data.AbilityExists(ability)) return;
        AbilityData data = null;
        if (ability.type == AbilitySettings.AbilityType.Active)
            data = new ActiveAbilityData(ability as ActiveAbility);
        else
            data = new PassiveAbilityData(ability as PassiveAbility);
        this.data.AddAbilityData(data);
        SendAbilityEvent(SpellManagerData.AbilitySendEvent.OnAbilityAdded, ability);
        if (ability.type == AbilitySettings.AbilityType.Passive)
            UseAbility(data);                                                               // передча данные(обертку) способности в метод UseAbility
    }

    /// <summary>
    /// ищу обертку способности которую хотим удалить через метод FindAbilityData если обертка существует - значит удал€ем ее через метод RemoveAbilityData и напоследок отсылаем сообщение об удалении.
    /// </summary>
    /// <param name="ability"></param>

    public void RemoveAbility(AbilityObject ability)
    {
        AbilityData data = this.data.FindAbilityData(ability);
        if (data == null) return;
        this.data.RemoveAbilityData(data);
        SendAbilityEvent(SpellManagerData.AbilitySendEvent.OnAbilityRemoved, ability);
        if (ability.type == AbilitySettings.AbilityType.Passive)
            this.StartCoroutine(FinalizeAbility(data));
    }

    public void UseAbility(AbilityObject ability)
    {

    }

    private void UseAbility(AbilityData data)
    {
        SendAbilityEvent(SpellManagerData.AbilitySendEvent.OnAbilityUsed, data.ability);
        this.StartCoroutine(InitializeAbility(data));
    }


    #region initializa and fenialize

    private IEnumerator InitializeAbility(AbilityData data)
    {

        if (data.isUsing)
        {
            this.StartCoroutine(FinalizeAbility(data));
            yield return new WaitForFixedUpdate();
        }

        data.isUsing = true;
        IAbilityComponentable compSys = data.ability as IAbilityComponentable;
        if (compSys == null) yield break;                                                       //  есть ли вообще у этой способности компоненты
                                                                                                //  с помощью интерфейса IAbilityComponentabl
        foreach (AbilityComponent component in compSys.components)                              // помощью цикла перебираю все компоненты в способности и с помощью метода SendComponentEvent отсылаю ее дальше объектам.
        {

            if (component.CheckForAttribute<DisableComponentMultiplyUsage>())
                SpecialComponentExpansion(component, data);
            else
                SendComponentEvent(SpellManagerData.ComponentSendEvent.OnComponentExpansion, component);

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator FinalizeAbility(AbilityData data)
    {
        SendAbilityEvent(SpellManagerData.AbilitySendEvent.OnAbilityEndUse, data.ability);                  // вызов событи€ OnAbilityEndUse
        IAbilityComponentable compSys = data.ability as IAbilityComponentable;
        data.isUsing = false;
        if (compSys == null) yield break;
        foreach (AbilityComponent component in compSys.components)                                          // с помощью цикла отправл€ю компоненты в метод SendComponentEvent который и отошлет компонент через событие.
        {
            if (component.supportRexpansion)
                SendComponentEvent(SpellManagerData.ComponentSendEvent.OnComponentRexpansion, component);
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion

    protected void SendAbilityEvent(SpellManagerData.AbilitySendEvent e, AbilityObject ability)
    {
        if (this.objectSpellBihaviour == null)
        {
            this.SendMessage(e.ToString(), ability, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            switch (e)
            {
                case SpellManagerData.AbilitySendEvent.OnAbilityAdded:
                    this.objectSpellBihaviour.OnAbilityAdded(ability);
                    break;
                case SpellManagerData.AbilitySendEvent.OnAbilityRemoved:
                    this.objectSpellBihaviour.OnAbilityRemoved(ability);
                    break;
                case SpellManagerData.AbilitySendEvent.OnAbilityUsed:
                    this.objectSpellBihaviour.OnAbilityUsed(ability);
                    break;
                case SpellManagerData.AbilitySendEvent.OnAbilityEndUse:
                    this.objectSpellBihaviour.OnAbilityEndUse(ability);
                    break;
            }
        }
    }

    // метод SendComponentEvent который будет отправл€ть сообщени€ вместе с компонентом.
    protected void SendComponentEvent(SpellManagerData.ComponentSendEvent e, AbilityComponent component)
    {
        if (this.objectSpellBihaviour == null)
        {
            this.SendMessage(e.ToString(), component, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            switch (e)
            {
                case SpellManagerData.ComponentSendEvent.OnComponentExpansion:
                    this.objectSpellBihaviour.OnComponentExpansion(component);
                    break;
                case SpellManagerData.ComponentSendEvent.OnComponentRexpansion:
                    this.objectSpellBihaviour.OnComponentRexpansion(component);
                    break;
            }
        }
    }
}



#region TimedComponentData 



public sealed class TimedComponentData
{

    public readonly AbilityData data = null;
    public readonly AbilityTimeComponent timeComponent = null;

    public float startTime = 0f;

    public TimedComponentData(AbilityData data, AbilityTimeComponent component)
    {
        this.data = data;
        this.timeComponent = component;
    }

}

#endregion