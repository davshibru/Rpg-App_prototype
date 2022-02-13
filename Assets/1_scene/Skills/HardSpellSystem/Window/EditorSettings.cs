using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorSettings
{
    public static Vector2 WindowSize = new Vector2(250, 200);

    [MenuItem("Window/Spell editor")]
    public static void InitWindow()
    {
        SpellWindow window = (SpellWindow) EditorWindow.GetWindow(typeof(SpellWindow));
        window.maxSize = window.minSize = WindowSize;
        if (SpellWindow.defStyle == null)
        {
            SpellWindow.defStyle = new GUIStyle();
            SpellWindow.defStyle.alignment = TextAnchor.LowerCenter;
            SpellWindow.defStyle.fontStyle = FontStyle.Bold;
        }
    }

    public static AbilityObject CreateAbility(AbilitySettings.AbilityType type)
    {
        AbilityObject ability = null;
        switch (type)
        {
            case AbilitySettings.AbilityType.Active:
                ability = new ActiveAbility("New Active");
                break;
            case AbilitySettings.AbilityType.Passive:
                ability = new PassiveAbility("New Passive");
                break;
        }
        return ability;
    }

    public static AbilityObject CreateCustomAbility(AbilitySettings.AbilityType type)
    {
        AbilityObject ability = null;
        switch (type)
        {
            case AbilitySettings.AbilityType.Active:
                ability = new ActiveCustomAbility("New Active custom");
                break;
            case AbilitySettings.AbilityType.Passive:
                ability = new PassiveCustomAbility("New Passive custom");
                break;
        }
        return ability;
    }

}

