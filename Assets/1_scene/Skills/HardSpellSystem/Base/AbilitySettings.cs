using System;
using UnityEngine;

public static class AbilitySettings
{
    public enum AbilityTimedType
    {
        Once,
        Repeat
    }

    public enum AbilityType
    {
        Active,
        Passive
    }

    
    public enum ComponentValueTarget : byte
    {
        [ComponentValueType(typeof(AbilityObjectComponent))] Special = 0,
        [ComponentValueType(typeof(AbilityFloatComponent))] Health = 1,
        [ComponentValueType(typeof(AbilityFloatComponent))] Mana = 2,
        [ComponentValueType(typeof(AbilityFloatComponent))] Armor = 3,
        [ComponentValueType(typeof(AbilityTimeComponent))] Time = 4,
        [ComponentValueType(typeof(AbilityBoolComponent))] Invis = 5,
        [ComponentValueType(typeof(AbilityFloatComponent))] MoveSpeed = 6,
    }

    public static AbilityComponent CreateComponentForTarget(ComponentValueTarget target, string name)
    {
        AbilityComponent component = new AbilityComponentValue<System.Object>(name, target); // ������� ��������� ��������� AbilityValueComponent � ������������� ����� System.Object
        Type type = ComponentValueType.GetValueType(target);                                 // � ������� ������ GetValueType "����" � ����� ��� ���������� ������� ��� "����" ������������
        try
        {
            component = (AbilityComponent)Activator.CreateInstance(type, name, target);      // � ������� Activator.CreateInstance ������ ��������� ����������� ����� ����
        }
        catch
        {
            Debug.LogError("Error: faild to initialize type: " + type);                      // ���� � ���������� ����� �� ����������� ��� ��������� � ������������ �� ��� �������� ���������� ������� ������ 
        }
        return component;
    }
}
