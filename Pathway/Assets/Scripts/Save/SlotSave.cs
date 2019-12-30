using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SlotSave
{

    private static readonly SlotSave instance = new SlotSave();
    
    static SlotSave()
    {

    }
    private SlotSave()
    {

    }
    public static SlotSave Instance
    {
        get
        {
            return instance;
        }
    }
    public void Savegame(SlotManager SM)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string path = Application.dataPath + "/UI" + "/uwu.oof";
        FileStream fs = new FileStream(path, FileMode.Create);

        SaveSlot SS = new SaveSlot(SM);
        bf.Serialize(fs,SS);
        fs.Close();

    }
    public SaveSlot Loadgame()
    {
        string path = Application.dataPath + "/UI" + "/uwu.oof";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            SaveSlot ss = bf.Deserialize(fs) as SaveSlot;
            fs.Close();
            return ss;
        }
        else
        {
            return null;
        }
    }

}
