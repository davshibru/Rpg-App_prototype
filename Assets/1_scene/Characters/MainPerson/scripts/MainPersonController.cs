using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPersonController : MonoBehaviour
{

    public FixedJoystick LeftJoystick;
    public FixedButton Button;
    public FixedTouchField TouchField;

    public int jump = 2; //Высота прыжка

    protected ActionsOfMainCharacter actionsOfMainCharacter;
    protected Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.2f;

    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    protected bool fightMod = false;

    // Start is called before the first frame update
    void Start()
    {
        actionsOfMainCharacter = GetComponent<ActionsOfMainCharacter>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector3(LeftJoystick.Direction.x, 0, LeftJoystick.Direction.y);
        var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input;

        fightMod = actionsOfMainCharacter.getFightMod();

        if (fightMod)
            vel *= 2f;
        else
            vel *= 5f;

        Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
        
        if (Rigidbody.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.AngleAxis(CameraAngleY + 180 + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);

        }


        CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 2, 2);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up - (Camera.main.transform.position), Vector3.up);


        if (Button.Pressed)
        {
            actionsOfMainCharacter.Jump();
            transform.position = transform.position + new Vector3(0, jump * 0.02f, 0);
        }


        if (Rigidbody.velocity.magnitude > 3f)
        {
            actionsOfMainCharacter.Run();
        }

        else if (Rigidbody.velocity.magnitude > 0.3f)
        {
            actionsOfMainCharacter.Walk();
        }

        else
        {
            actionsOfMainCharacter.Stay();
        }
    }

    
}
