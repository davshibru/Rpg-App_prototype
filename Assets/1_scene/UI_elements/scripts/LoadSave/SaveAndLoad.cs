using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad : MonoBehaviour
{

    public GameObject Player;
    public GameObject ImageSaveAndLoad;
    public GameObject PauseMenu;

    public Text SloatName1;
    public Text SloatName2;

    public Button Save;
    public Button Load;
    public Button Delete;

    public Button CurentClickButton;


    [System.Serializable]
    public class StringName
    {
        public string NameSave;
        public float x;
        public float y;
        public float z;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void ClickSlots(Button but)
    {
        Save.gameObject.SetActive(true);
        Load.gameObject.SetActive(true);
        Delete.gameObject.SetActive(true);
        CurentClickButton = but;

        

    }

    public void Back()
    {
        ImageSaveAndLoad.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void Saves()
    {
        SlotConfig slotConfig = CurentClickButton.GetComponent<SlotConfig>();
        slotConfig.Taken = true;
        StringName stringName = new StringName();
        stringName.NameSave = slotConfig.CurNumBut.ToString();
        stringName.x = Player.transform.position.x;
        stringName.y = Player.transform.position.y;
        stringName.z = Player.transform.position.z;

        if (!File.Exists(Application.dataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/saves");
            if (slotConfig.CurNumBut == 1)
            {
                FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, stringName);
                SloatName1.text = "Сохранение" + stringName.NameSave;
                fs.Close();
            }
            if (slotConfig.CurNumBut == 2)
            {
                FileStream fs = new FileStream(Application.dataPath + "/saves/save1.bot", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, stringName);
                SloatName2.text = "Сохранение" + stringName.NameSave;
                fs.Close();
            }
        }
    }

    public void Loads()
    {
        SlotConfig slotConfig = CurentClickButton.GetComponent<SlotConfig>();
        if (slotConfig.CurNumBut == 1)
        {
            if (File.Exists(Application.dataPath + "/saves/save.bot"))
            {
                FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                StringName stringName = (StringName)formatter.Deserialize(fs);
                Player.transform.position = new Vector3(stringName.x, stringName.y, stringName.z);
                fs.Close();
            }
        }
        if (slotConfig.CurNumBut == 2)
        {
            if (File.Exists(Application.dataPath + "/saves/save1.bot"))
            {
                FileStream fs = new FileStream(Application.dataPath + "/saves/save1.bot", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                StringName stringName = (StringName)formatter.Deserialize(fs);
                Player.transform.position = new Vector3(stringName.x, stringName.y, stringName.z);
                fs.Close();
            }
        }
    }

    public void Deleted()
    {
        SlotConfig slotConfig = CurentClickButton.GetComponent<SlotConfig>();
        if (slotConfig.CurNumBut == 1)
        {
            if (File.Exists(Application.dataPath + "/saves/save.bot"))
            {
                File.Delete(Application.dataPath + "/saves/save.bot");
                SloatName1.text = "Пустой слот";
            }
        }
        if (slotConfig.CurNumBut == 2)
        {
            if (File.Exists(Application.dataPath + "/saves/save1.bot"))
            {
                File.Delete(Application.dataPath + "/saves/save1.bot");
                SloatName2.text = "Пустой слот";
            }
        }
    }


    public void ShowPanelSlots()
    {
        ImageSaveAndLoad.SetActive(true);
        PauseMenu.SetActive(false);

        if (File.Exists(Application.dataPath + "/saves/save.bot"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save.bot", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            StringName stringName = (StringName)formatter.Deserialize(fs);
            SloatName1.text = "Сохранение" + stringName.NameSave;
            fs.Close();
        }
        if (File.Exists(Application.dataPath + "/saves/save1.bot"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save1.bot", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            StringName stringName = (StringName)formatter.Deserialize(fs);
            SloatName2.text = "Сохранение" + stringName.NameSave;
            fs.Close();
        }
    }

}
