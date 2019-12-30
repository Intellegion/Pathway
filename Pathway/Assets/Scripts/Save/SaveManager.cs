using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SaveManager<T>
{
    private static readonly SaveManager<T> instance = new SaveManager<T>();

    static SaveManager()
    {

    }
    private SaveManager()
    {

    }
    public static SaveManager<T> Instance 
    {
        get
        {
            return instance;
        }
    }
}
