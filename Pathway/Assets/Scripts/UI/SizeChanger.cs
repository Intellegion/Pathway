using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SizeChanger : MonoBehaviour
{

    public void ChangeX(TMP_InputField X)
    {
        MazeGen.sizeX = Int32.Parse(X.text);
    }
    public void ChangeY(TMP_InputField Y)
    {
        MazeGen.sizeY = Int32.Parse(Y.text);
    }
    public void DestX(TMP_InputField X)
    {
        LevelEditor.destX = Int32.Parse(X.text);
    }
    public void DestY(TMP_InputField Y)
    {
        LevelEditor.destY = Int32.Parse(Y.text);
    }
   
}
