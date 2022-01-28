using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingWeapon : MonoBehaviour
{

    public int currentNumberWeapon;
    public GameObject[] objectWeapons;

    //private bool isFightMode = false;

    public FixedButton FightModeButton;
    public FixedButton NormalModeButton;
    public FixedButton ChangeWeaponButton;

    public GameObject curentWeapon;
    public Transform leftHand;

    public int AttackPlayer;
    public int DefendPlayer;

    public GameObject normalMode;
    public GameObject fightMode;


    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void FightModeButtonMethod()
    {
        normalMode.SetActive(false);
        fightMode.SetActive(true);
        changingWeaponMethod();
    }

    public void NormalModeButtonMethod()
    {
        //isFightMode = true;
        normalMode.SetActive(true);
        fightMode.SetActive(false);

        currentNumberWeapon = 0;
        Destroy(curentWeapon);
    }

    public void ChangeWeaponButtonMethod()
    {
        changingWeaponMethod();
    }

    private void changingWeaponMethod()
    {
        currentNumberWeapon += 1;

        if (currentNumberWeapon == 2)
        {
            currentNumberWeapon = 0;
        }

        if (objectWeapons[currentNumberWeapon] != null)
        {
            if (curentWeapon != null)
            {
                Destroy(curentWeapon);
            }

            curentWeapon = (GameObject)Instantiate(objectWeapons[currentNumberWeapon]);
            curentWeapon.transform.parent = leftHand;
            curentWeapon.transform.localPosition = Vector3.zero;
            curentWeapon.transform.localRotation = Quaternion.Euler(-90, 180, -260);

        }

        AttackPlayer = curentWeapon.GetComponent<WeaponStats>().Attacks;
        DefendPlayer = curentWeapon.GetComponent<WeaponStats>().Defends;
    }
}
