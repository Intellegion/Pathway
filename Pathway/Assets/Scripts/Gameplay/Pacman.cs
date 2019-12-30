using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Pacman : Modes
{
    private GameObject coin;
    private GameObject go;
    protected override void Initialize()
    {
        coin = Resources.Load<GameObject>("Prefabs/Test");
        for(int i=0;i< MazeGen.sizeX;i++)
        {
            for(int j=0;j<MazeGen.sizeY;j++)
            {
                go = Instantiate(coin, new Vector2(i,j), Quaternion.identity) as GameObject;
            }
        }
    }
    protected override void UpdateGame()
    {

    }
    
    
    
}
