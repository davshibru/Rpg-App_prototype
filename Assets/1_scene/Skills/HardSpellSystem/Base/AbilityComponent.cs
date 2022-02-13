using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


/// <summary>
/// базовый класс
/// </summary>
public abstract class AbilityComponent
{
    public string name = "";
    public AbilitySettings.ComponentValueTarget targetValue { get; protected set; }
    public bool supportRexpansion = true;


    public virtual void DrawComponentBase()
    {
#if UNITY_EDITOR
        this.name = EditorGUILayout.TextField("Name: ", this.name);
        this.supportRexpansion = EditorGUILayout.Toggle("Allow to set back value", this.supportRexpansion);
#endif
    }

    public bool CheckForAttribute<T>() where T : Attribute
    {
        foreach (Attribute attribute in Attribute.GetCustomAttributes(this.GetType()))
        {
            if (attribute is T) return true;
        }
        return false;
    }

    public abstract System.Object objectValue { get; }
}




/// link -  http://unity3d.ru/distribution/viewtopic.php?f=11&t=29415&p=195386#p195386

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public sealed class ComponentValueType : Attribute
{

    public Type type;

    public ComponentValueType(Type type)
    {
        this.type = type;
    }

    public static Type GetValueType(AbilitySettings.ComponentValueTarget value)
    {
        Type t = typeof(System.Object);
        FieldInfo info = value.GetType().GetField(value.ToString());
        ComponentValueType attribute = (ComponentValueType)Attribute.GetCustomAttribute(info, typeof(ComponentValueType));
        if (attribute != null) t = attribute.type;
        return t;
    }

}
