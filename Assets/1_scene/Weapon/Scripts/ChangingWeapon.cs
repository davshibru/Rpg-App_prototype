using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingWeapon : MonoBehaviour
{

    public int currentNumberWeapon;
    public GameObject[] objectWeapons;

    //private bool isFightMode = false;

    /*public FixedButton FightModeButton;
    public FixedButton NormalModeButton;
    public FixedButton ChangeWeaponButton;
    */
    protected ActionsOfMainCharacter actionsOfMainCharacter;

    public GameObject curentWeapon;
    public Transform rightHand;

    public int AttackPlayer;
    public int DefendPlayer;

    public GameObject normalMode;
    public GameObject fightMode;

    void Start()
    {
        actionsOfMainCharacter = GetComponent<ActionsOfMainCharacter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void FightModeButtonMethod()
    {
        normalMode.SetActive(false);
        fightMode.SetActive(true);
        actionsOfMainCharacter.TakeOnWeapon();
        changingWeaponMethod();
    }

    public void NormalModeButtonMethod()
    {
        //isFightMode = true;
        normalMode.SetActive(true);
        fightMode.SetActive(false);
        actionsOfMainCharacter.TakeOutWeapon();
        //currentNumberWeapon = 0;
        Destroy(curentWeapon);
    }

    public void ChangeWeaponButtonMethod()
    {
        currentNumberWeapon += 1;
        changingWeaponMethod();
    }

    private void changingWeaponMethod()
    {
        

        if (currentNumberWeapon == objectWeapons.Length)
        {
            currentNumberWeapon = 0;
        }

        if (objectWeapons[currentNumberWeapon] != null)
        {
            if (curentWeapon != null)
            {
                switch (curentWeapon.GetComponent<WeaponStats>().WeaponTag)
                {
                    case "sword":

                        actionsOfMainCharacter.setOutSwordMode();

                        break;
                    case "axe":
                        break;
                }


                Destroy(curentWeapon);
            }

            curentWeapon = (GameObject)Instantiate(objectWeapons[currentNumberWeapon]);
            curentWeapon.transform.parent = rightHand;
            curentWeapon.transform.localPosition = Vector3.zero;
            curentWeapon.transform.localRotation = Quaternion.Euler(90, -90, 0);

            switch (curentWeapon.GetComponent<WeaponStats>().WeaponTag)
            {
                case "sword":
                    actionsOfMainCharacter.setSwordMode();
                    break;

                case "axe":
                    break;
            }

        }

        AttackPlayer = curentWeapon.GetComponent<WeaponStats>().Attacks;
        DefendPlayer = curentWeapon.GetComponent<WeaponStats>().Defends;
    }
}
