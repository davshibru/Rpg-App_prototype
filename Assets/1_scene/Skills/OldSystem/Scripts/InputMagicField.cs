using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PDollarGestureRecognizer;

public class InputMagicField : MonoBehaviour
{
    private int sizeOfMagicSloats = 2;
    [HideInInspector]
    public bool miniganMode = false;
    public bool drawedMagicRune = false;

    private bool slotThreeMagic = false;

    private string skill = "from above from above ";

    // scroll panel

    public GameObject ScrollInstance;


    // buttons
    public GameObject[] skillsButtons;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void magicAtackWithOutStop(Button but)
    {
        miniganMode = true;
    }

    public void fullingTreeMagicInSlot(Button but)
    {
        slotThreeMagic = true;
    }

    
    public void drawMagic()
    {
        miniganMode = true;
        drawedMagicRune = true;
        miniganMode = false;
    }
    


    public void addMagicInSlot()
    {

        int currentSizeOfMagicSlots = ScrollInstance.GetComponent<Transform>().childCount;
        
        if (currentSizeOfMagicSlots <= sizeOfMagicSloats)
        {
            addInSlotMethod(false);
            
        }
        else
        {
            
            addInSlotMethod(true);
            
        }
        
    }

    private void addInSlotMethod(bool full)
    {

        for(int i = 0; i < skillsButtons.Length; i++)
        {
            if (skillsButtons[i].GetComponent<SkillsStats>().command.Equals(skill))
            {
                if (full)
                    Destroy(ScrollInstance.GetComponentInChildren<Transform>().GetChild(0).gameObject);

                Button buttonObj = skillsButtons[i].GetComponent<SkillsStats>().skillButton;
                GameObject obj = Instantiate(buttonObj.transform.gameObject);
                obj.transform.SetParent(ScrollInstance.transform);
            }
        }
    }

}
