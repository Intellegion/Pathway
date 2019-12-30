using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grids
{
    public int spawnX, spawnY; //x,y coordinates of the grid
    public bool[] dir = new bool[4];
    public int g, h, f;
    public Grids(int i,int j)
    {
        spawnX = i;
        spawnY = j;
    }
    public Grids()
    {
        
    }
}
