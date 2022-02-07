using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSystem : MonoBehaviour
{

    public List<Spell> Spells;
    public int spellID = 0;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeMagic(Transform tr)
    {
        spellID = 0;
        Spells[spellID].A_ActivateSpell(tr);
    }
}
