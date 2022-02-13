using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#region base class

/// <summary>
/// УНИВЕРСАЛЬНЫЙ компонент в который может быть записан любой тип значения - следовательно этот компонент будет использовать ПРЕОБРАЗОВАНИЕ.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AbilityComponentValue<T> : AbilityComponent
{
    public T value;
    public string valueName = "";

    public sealed override System.Object objectValue
    {
        get { return this.value as System.Object; }
    }

    public AbilityComponentValue(string name, AbilitySettings.ComponentValueTarget target)
    {
        this.name = name;
        this.targetValue = target;
        this.valueName = this.targetValue.ToString();
    }
}
    

    #endregion


#region Шаблоны классов

    //  шаблоны компонентов с определенным типом данных

public class AbilityFloatComponent : AbilityComponentValue<float>
{

    public AbilityFloatComponent(string name, AbilitySettings.ComponentValueTarget target) : base(name, target) { }

    public override void DrawComponentBase()
    {
        base.DrawComponentBase();
    #if UNITY_EDITOR
            this.value = EditorGUILayout.FloatField(this.valueName, this.value);
    #endif
    }

}

public class AbilityIntComponent : AbilityComponentValue<int>
{

    public AbilityIntComponent(string name, AbilitySettings.ComponentValueTarget target) : base(name, target) { }

    public override void DrawComponentBase()
    {
        base.DrawComponentBase();
#if UNITY_EDITOR
        this.value = EditorGUILayout.IntField(this.valueName, this.value);
#endif
    }

}

public class AbilityStringComponent : AbilityComponentValue<string>
{

    public AbilityStringComponent(string name, AbilitySettings.ComponentValueTarget target) : base(name, target) { }


    public override void DrawComponentBase()
    {
        base.DrawComponentBase();
#if UNITY_EDITOR
        this.value = EditorGUILayout.TextField(this.valueName, this.value);
#endif
    }

}

public class AbilityObjectComponent : AbilityComponentValue<UnityEngine.Object>
{

    public AbilityObjectComponent(string name, AbilitySettings.ComponentValueTarget target) : base(name, target) { }

}

public class AbilityBoolComponent : AbilityComponentValue<bool>
{
    
    public AbilityBoolComponent(string name, AbilitySettings.ComponentValueTarget target) : base(name, target) { }

    public override void DrawComponentBase()
    {
        base.DrawComponentBase();
#if UNITY_EDITOR
        this.value = EditorGUILayout.Toggle(this.valueName, this.value);
#endif
    }

}


#endregion





[DisableComponentMultiplyUsage]
public class AbilityTimeComponent : AbilityFloatComponent
    {

    private int componentRepeatTimes = 0;

    public AbilitySettings.AbilityTimedType timeType;

    public override void DrawComponentBase()
    {
        base.DrawComponentBase();
#if UNITY_EDITOR
        this.timeType = (AbilitySettings.AbilityTimedType)EditorGUILayout.EnumPopup("Timed type: ", this.timeType);
        if (this.timeType == AbilitySettings.AbilityTimedType.Repeat)
        {
            this.repeatTimes = EditorGUILayout.IntField("Repeat times: ", this.repeatTimes);
        }
#endif
    }

    public int repeatTimes
    {
        get { return this.componentRepeatTimes; }
        set { this.componentRepeatTimes = (value < 0) ? 0 : value; }
    }

    public float time
    {
        get { return Mathf.Abs(this.value); }
        set { this.value = (value < 0f) ? 0f : value; }
    }

    public AbilityTimeComponent(string name) : base(name, AbilitySettings.ComponentValueTarget.Time) { }

    
}