using System;
using UnityEngine;
#if UNITY_EDITOR      /// UNITY_EDITOR - эта область будет давать понять самому редактору unity3D что мы
/// работаем в режиме редактора и только в этом режиме, тоесть весь код который заключен в эту область не войдет в финальный билд
/// приложения.
using UnityEditor;
using System.IO;
public class SpellWindow : EditorWindow
{
    public static GUIStyle defStyle = null;                             // стиль по умолчанию, чисто визуальная корректировка, чтобы текст отображался симпатичнее.

    private AbilityObject localAbility = null;                          // объект способности, будет служить нам переменной в которой будет содержаться редактируемая способность.
    private IAbilityComponentable abilitiesComponent = null;            // интерфейс IAbilityComponentable объекта способности в котором мы будет содержать инструменты для работы с компонентами.
    private AbilitySettings.ComponentValueTarget localComponentType;    // тип компонента который пользователь захочет добавить.


    
    private void OnGUI()
    {
        if (this.isEditor)
        {
            UpdateWindowGUI();
        }
    }

    private void OnInspectorUpdate()
    {
        if (this.isEditor)
        {
            if (this.abilitiesComponent != null)
            {
                EditorSettings.WindowSize.y = 250 + (100 * this.abilitiesComponent.components.Length);
                this.maxSize = this.minSize = EditorSettings.WindowSize;
                if (this.abilitiesComponent.components.Length <= 0)
                {
                    this.abilitiesComponent = null;
                    this.localAbility = EditorSettings.CreateAbility(this.localAbility.type);
                }
            }
        }
    }

    private void UpdateWindowGUI()
    {
        if (localAbility == null) DrawMainStartGUI();
        else
        {
            DrawAbilityControl();
            GUILayout.Space(10);
            DrawControlState();
        }
    }

    private void DrawAbilityMainSettings(AbilityObject ability)
    {
        Rect r = GUILayoutUtility.GetRect(EditorSettings.WindowSize.x, 20);
        GUI.Box(r, "Settings of: " + ability.name + " ability");
        GUILayout.Label("Abilities description", defStyle);
        ability.abilityDescription = GUILayout.TextArea(ability.abilityDescription);
        ability.name = EditorGUILayout.TextField("Name: ", ability.name);
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Icon: ");
        ability.icon = (Texture2D)EditorGUILayout.ObjectField(ability.icon, typeof(Texture2D), false);
        GUILayout.EndHorizontal();
        if (ability.type == AbilitySettings.AbilityType.Active)
        {
            ActiveAbility activeAbility = (ActiveAbility)ability;
            activeAbility.castButton = (FixedButton)EditorGUILayout.ObjectField("Cast button",activeAbility.castButton, typeof(FixedButton), false );
            activeAbility.cooldown = EditorGUILayout.FloatField("Cooldown: ", activeAbility.cooldown);
        }
    }

    private void DrawAbilityControl()
    {
        DrawAbilityMainSettings(this.localAbility);
        Rect r = GUILayoutUtility.GetRect(EditorSettings.WindowSize.x, 15);
        GUILayout.Space(5);
        r.height += 50;
        GUI.Box(r, "Ability components settings");
        this.localComponentType = (AbilitySettings.ComponentValueTarget)EditorGUILayout.EnumPopup("Select component type: ", this.localComponentType);
        GUILayout.BeginHorizontal();
        if (this.abilitiesComponent != null)
        {
            if (GUILayout.Button("Add component")) TryToAddComponent();
            if (GUILayout.Button("Remove all")) this.abilitiesComponent.RemoveComponents<AbilityComponent>();
        }
        else
        {
            if (GUILayout.Button("Add component")) TryToAddComponent();
        }
        GUILayout.EndHorizontal();
        GUILayoutUtility.GetRect(EditorSettings.WindowSize.x, 5);
        if (this.abilitiesComponent != null)
        {
            DrawAbilitiesComponents();
        }
    }

    private void DrawAbilitiesComponents()
    {
        if (this.abilitiesComponent == null) return;
        int count = this.abilitiesComponent.components.Length;
        GUILayout.Label("Ability components count: " + count, defStyle);
        GUILayout.Space(5);
        foreach (AbilityComponent component in this.abilitiesComponent.components)
        {
            DrawComponentSettings(component);
        }
    }

    private void DrawComponentSettings(AbilityComponent component)
    {
        Rect r = GUILayoutUtility.GetRect(EditorSettings.WindowSize.x, 20);
        GUI.Box(r, component.name);
        component.DrawComponentBase();
        if (GUILayout.Button("Remove component: " + component.targetValue)) TryToRemoveComponent(component);
    }

    private void TryToRemoveComponent(AbilityComponent component)
    {
        if (this.abilitiesComponent != null)
            this.abilitiesComponent.RemoveComponent(component);
    }

    private void TryToRemoveComponent(int componentIndex)
    {
        if (componentIndex < 0 || this.abilitiesComponent == null) return;
        else if (componentIndex >= this.abilitiesComponent.components.Length) return;

        TryToRemoveComponent(this.abilitiesComponent.components[componentIndex]);
    }

    private void TryToAddComponent()
    {
        if (this.abilitiesComponent == null)
        {
            string oldName = this.localAbility.name;
            this.localAbility = EditorSettings.CreateCustomAbility(this.localAbility.type);
            this.localAbility.name = oldName;
            this.abilitiesComponent = this.localAbility as IAbilityComponentable;
            if (this.abilitiesComponent != null) TryToAddComponent();
        }
        else
        {
            AbilityComponent com = AbilitySettings.CreateComponentForTarget(this.localComponentType, "New component");
            this.abilitiesComponent.AddComponent(com);
        }
    }

    private void TryToSave()
    {

    }

    private void DrawMainStartGUI()
    {
        Rect r = GUILayoutUtility.GetRect(EditorSettings.WindowSize.x, 15);
        r.height += 25;
        GUI.Box(r, "Create new ability");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Passive")) this.localAbility = EditorSettings.CreateAbility(AbilitySettings.AbilityType.Passive);
        if (GUILayout.Button("Active")) this.localAbility = EditorSettings.CreateAbility(AbilitySettings.AbilityType.Active);
        GUILayout.EndHorizontal();
    }

    private void DrawControlState()
    {
        Rect r = GUILayoutUtility.GetRect(EditorSettings.WindowSize.x, 2.5f);
        r.height += 27.5f;
        GUI.Box(r, "");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save: " + this.localAbility.name)) TryToSave();
        if (GUILayout.Button("Delete")) { }
        GUILayout.EndHorizontal();
    }

    public bool isEditor
    {
        get { return Application.isPlaying == false; }
    }

}


#endif