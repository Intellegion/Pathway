using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class SaveSystem 
{

    private static readonly SaveSystem instance = new SaveSystem();

    static SaveSystem()
    {

    }
    private SaveSystem()
    {

    }
    public static SaveSystem Instance
    {
        get
        {
            return instance;
        }
    }

    public void Savegame(MazeGen mg, string name)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string path = Application.dataPath + "/Levels/" +name+ ".oof";
        FileStream fs = new FileStream(path, FileMode.Create);

        MazeData m = new MazeData(mg);
        bf.Serialize(fs,m);
        fs.Close();

    }
    public MazeData Loadgame(string name)
    {
        string path = Application.dataPath + "/Levels/"+name+".oof";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            MazeData m = bf.Deserialize(fs) as MazeData;
            fs.Close();
            return m;
        }
        else
        {
            return null;
        }
    }
}