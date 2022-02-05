using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_controll : MonoBehaviour
{

    public GameObject normalMode;
    public GameObject magicMode;

    public fieldMagicInput fieldMagicInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void turnOnMagicMode()
    {
        normalMode.SetActive(false);
        magicMode.SetActive(true);
    }

    public void turnOnNormalMode()
    {
        normalMode.SetActive(true);
        magicMode.SetActive(false);
        fieldMagicInput.clearField();
    }

}
