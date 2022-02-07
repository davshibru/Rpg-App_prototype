using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetiorShower : Spell
{
    [Header("Status")]
    public bool Status = false;

    [Header("Attributs")]
    public float Damage;
    public float Radius;
    public float Duration;
    public float Interval;

    public int countPerWave;

    [Header("Ref")]
    public GameObject epicenterPrefab;

    private Vector3 p_pointCast;
    private Quaternion p_rotete;

    [SerializeField]
    private AttributeSpell _attribute;
    public override AttributeSpell A_Attribute { get { return _attribute; } set { _attribute = value; } }

    public override void A_ActivateSpell(Transform tr)
    {
        p_pointCast = new Vector3(tr.position.x, tr.position.y + 0.5f, tr.position.z);
        p_rotete = tr.rotation;
        Status = true;
    }


    private void Update()
    {
        if (Status)
        {
            var temp = Instantiate(epicenterPrefab, p_pointCast, p_rotete).GetComponent<Ephicentr>();
            temp.Init(Damage, Duration, Radius, Interval, countPerWave, p_pointCast, p_rotete);
            Status = false;
        }
        
    }

}
