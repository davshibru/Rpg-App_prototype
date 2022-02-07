using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attribute", menuName = "Spell/Attribute")]
public class AttributeSpell : ScriptableObject
{
    [Header("Description")]
    public string Name;
    public Sprite Icon;

    [Header("Attribute")]
    public float Cooldown;
    public int ManaCost;

} 
