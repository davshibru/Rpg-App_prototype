using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Attribute", menuName = "Spell/Attribute")]
public class AttributeSpell : ScriptableObject
{
    [Header("Description")]
    public string Name;
    public Sprite Icon;
    public Button Button;

    [Header("Attribute")]
    public float Cooldown;
    public int ManaCost;

} 
