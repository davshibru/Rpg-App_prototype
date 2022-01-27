using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personController : MonoBehaviour
{

    public FixedJoystick LeftJoystick;
    public FixedTouchField TouchField;

    protected ActionsOfMainCharacter actionsOfMainCharacter;
    protected Rigidbody Rigidbody;

    

    // Start is called before the first frame update
    void Start()
    {
        actionsOfMainCharacter = GetComponent<ActionsOfMainCharacter>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
