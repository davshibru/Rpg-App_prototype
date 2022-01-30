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
    protected ChangingWeapon changingWeapon;
    protected SkillsControlls skillsControlls;


    protected Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.2f;

    protected float CameraPosY;
    protected float CameraPosSpeed = 0.005f;

    protected bool fightMod = false;

    public GameObject Skill;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        actionsOfMainCharacter = GetComponent<ActionsOfMainCharacter>();
        
        changingWeapon = GetComponent<ChangingWeapon>();
        skillsControlls = GetComponent<SkillsControlls>();
    }

    // Update is called once per frame
    void Update()
    {

        inputMoveAndCamera();

        /*
        if (Button.Pressed)
        {
            actionsOfMainCharacter.Jump();
            transform.position = transform.position + new Vector3(0, jump * 0.02f, 0);
        }*/


        callAnimationStayWalkAndRun();


    }

    public void jumpAction()
    {
        actionsOfMainCharacter.Jump();
        transform.position = transform.position + new Vector3(0, jump * 0.02f, 0);
    }

    public void attackAction()
    {
        switch (changingWeapon.curentWeapon.GetComponent<WeaponStats>().WeaponTag)
        {
            case "sword":
                actionsOfMainCharacter.makeAttackTriger();
                break;

            case "axe":
                break;
        }
    }


    public void magicAttacAction()
    {
        Instantiate(Skill, transform.position, transform.rotation);
        Destroy(Skill, Skill.GetComponent<SkillsControlls>().timer);
    }

    protected void inputMoveAndCamera()
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
        CameraPosY = Mathf.Clamp(CameraPosY - TouchField.TouchDist.y * CameraPosSpeed, 0, 3f);

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, CameraPosY, 2);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up - (Camera.main.transform.position), Vector3.up);

    }

    protected void callAnimationStayWalkAndRun()
    {
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
