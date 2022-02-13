using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSystem : MonoBehaviour
{

    public List<Spell> Spells;
    public int spellID = 0;

    public FixedButton meteorButton;

    public GameObject spellSloats;

    private bool topDawnView;
    private ChangingWeapon changingWeapon;

    private FixedButton sloat1;

    private int abilityId = 0;


    // Start is called before the first frame update
    void Start()
    {
        changingWeapon = GetComponent<ChangingWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpellSloats();

        if (sloat1.getPressed())
        {
            //makeMagic();
            topDawnView = true;
            abilityId = sloat1.id;
        }

        if (topDawnView)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.distance < 50f)
                    {
                        makeMagic(hitInfo.point);
                    }
                }
            }
        }

    }

    // Получение актуальных способностей из панели слотов
    private void UpdateSpellSloats()
    {
        
        int count = 0;
        for (int i = 0; i < spellSloats.transform.childCount; i++)
        {
            var button = spellSloats.transform.GetChild(i).gameObject.GetComponent<FixedButton>();
            if (button != null)
            {
                switch (count) {
                    case 0:
                        sloat1 = button;
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
                count++;
            }
            

            
        }

        
    }

    public void makeMagic(Vector3 vector)
    {
        spellID = 0;
        Spells[abilityId].A_ActivateSpell(vector);
        changingWeapon.ClickerCameraModeTurnOn();
        
    }


    public void setTopDownView(bool value)
    {
        topDawnView = value;
    }

    public bool getTopDownView()
    {
        return topDawnView;
    }

    
}
