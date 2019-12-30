using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGen 
{
    
    private GameObject wallobj;
    private GameObject enemyobj;

    public static int sizeX=100, sizeY=100; //size of the grid

    public static Grids[][] grids=new Grids[sizeX][];
    private Grids currentgrid;
    private List<int> allowed = new List<int>();
    public List<Grids> path = new List<Grids>(); //walkable area
    private List<Grids> visited = new List<Grids>(); //visited area
    private Directions dir;
    bool isRun=false;
    Transform t;
    public List<int> blueTiles = new List<int>();
    public List<int> yellowTiles = new List<int>();
    public List<int> greenTiles = new List<int>();
    public List<int> blackTiles = new List<int>();
    public List<int> redTiles = new List<int>();
    public List<int> blueShifters = new List<int>();
    public List<int> yellowShifters = new List<int>();
    public List<int> greenShifters = new List<int>();
    public List<int> redShifters = new List<int>();
    public GameObject[] enemies;
    public MazeGen(GameObject g, LevelEditor l)
    {
        t = l.transform;
        wallobj = g;
        for(int i = 0; i < sizeX; i++)
        {
            grids[i] = new Grids[sizeY];
        }
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                grids[i][j] = new Grids(i, j);
            }
        }
        enemyobj = Resources.Load<GameObject>("Prefabs/Enemy");
    }
    public MazeGen()
    {

    }
    public void mazeStart()
    {
        
        if (!isRun)
        {          
            path.Add(grids[0][0]);
            visited.Add(grids[0][0]);

            generator(0, 0);
            directions(0, 0);
            wallgen();
        }
    }
    void generator(int sx, int sy) //spawn values
    {
        int ret;
        allowed.Clear();
        allowed.Add(0);
        allowed.Add(1);
        allowed.Add(2);
        allowed.Add(3);
        if (sx == 0 && sy == 0)
        {
            allowed.Remove(1);
            allowed.Remove(3);
        }
        else if (sx == sizeX - 1 && sy == 0)
        {
            allowed.Remove(0);
            allowed.Remove(3);
        }
        else if (sx == 0 && sy == sizeY - 1)
        {
            allowed.Remove(1);
            allowed.Remove(2);
        }
        else if (sx == sizeX - 1 && sy == sizeY - 1)
        {
            allowed.Remove(0);
            allowed.Remove(2);
        }
        else if ((sx > 0 && sx < sizeX - 1) && sy == 0)
        {
            allowed.Remove(3);

        }
        else if ((sx > 0 && sx < sizeX - 1) && sy == sizeY - 1)
        {
            allowed.Remove(2);
        }
        else if ((sy > 0 && sy < sizeY - 1) && sx == 0)
        {
            allowed.Remove(1);
        }

        else if ((sy > 0 && sy < sizeY - 1) && sx == sizeX - 1)
        {
            allowed.Remove(0);
        }
        ret = allowed[Random.Range(0, allowed.Count)];
        dir = (Directions)ret;
    }
    void directions(int sx, int sy)
    {
        int flag=0;
        do
        {
            switch (dir)
            {
                case Directions.East:
                    {
                        if (!visited.Contains(grids[sx + 1][sy]))
                        {
                            grids[sx][sy].dir[0] = true;
                            sx++;
                            grids[sx][sy].dir[1] = true;
                            currentgrid = grids[sx][sy];
                            
                            visited.Add(grids[sx][sy]);
                            path.Add(grids[sx][sy]);

                            generator(sx,sy);
                            flag = 1;
                            break;

                        }
                        else
                        {
                            allowed.Remove(0);
                            if (allowed.Count == 0)
                            {
                                path.RemoveAt(path.Count - 1);
                                if (path[path.Count - 1].spawnX == 0 && path[path.Count-1].spawnY == 0)
                                {
                                    flag = 3;
                                    break;
                                }
                                sx = path[path.Count - 1].spawnX;
                                sy = path[path.Count - 1].spawnY;
                                generator(sx,sy);
                                flag = 2;
                                break;
                            }
                            else
                            {
                                dir = (Directions)allowed[Random.Range(0, allowed.Count)];
                                continue;
                            }
                        }
                    }
                case Directions.West:
                    {
                        if (!visited.Contains(grids[sx - 1][sy]))
                        {
                            grids[sx][sy].dir[1] = true;
                            sx--;
                            grids[sx][sy].dir[0] = true;
                            currentgrid = grids[sx][sy];
                            
                            visited.Add(grids[sx][sy]);
                            path.Add(grids[sx][sy]);

                            generator(sx,sy);
                            flag = 1;
                            break;
                        }
                        else
                        {
                            allowed.Remove(1);
                            if (allowed.Count == 0)
                            {
                                path.RemoveAt(path.Count - 1);
                                if (path[path.Count - 1].spawnX == 0 && path[path.Count-1].spawnY == 0)
                                {
                                    flag = 3;
                                    break;
                                }
                                sx = path[path.Count - 1].spawnX;
                                sy = path[path.Count - 1].spawnY;
                                generator(sx, sy);
                                flag = 2;
                                break;
                            }
                            else
                            {
                                dir = (Directions)allowed[Random.Range(0, allowed.Count)];
                                continue;
                            }
                        }
                    }
                case Directions.North:
                    {
                        if (!visited.Contains(grids[sx][sy + 1]))
                        {
                            grids[sx][sy].dir[2] = true;
                            sy++;
                            grids[sx][sy].dir[3] = true;
                            currentgrid = grids[sx][sy];
                            
                            visited.Add(grids[sx][sy]);
                            path.Add(grids[sx][sy]);

                            generator(sx, sy);
                            flag = 1;
                            break;
                        }
                        else
                        {
                            allowed.Remove(2);
                            if (allowed.Count == 0)
                            {
                                path.RemoveAt(path.Count - 1);
                                if (path[path.Count - 1].spawnX == 0 && path[path.Count-1].spawnY == 0)
                                {
                                    flag = 3;
                                    break;
                                }
                                sx = path[path.Count - 1].spawnX;
                                sy = path[path.Count - 1].spawnY;
                                generator(sx, sy);
                                flag = 2;
                                break;

                            }
                            else
                            {
                                
                                dir = (Directions)allowed[Random.Range(0, allowed.Count)];
                                continue;
                            }
                        }
                    }
                case Directions.South:
                    {
                        if (!visited.Contains(grids[sx][sy - 1]))
                        {
                            grids[sx][sy].dir[3] = true;
                            sy--;
                            grids[sx][sy].dir[2] = true;
                            currentgrid = grids[sx][sy];
                            
                            visited.Add(grids[sx][sy]);
                            path.Add(grids[sx][sy]);

                            generator(sx, sy);
                            flag = 1;
                            break;
                        }
                        else
                        {
                            allowed.Remove(3);
                            if (allowed.Count == 0)
                            {
                                path.RemoveAt(path.Count - 1);
                                if (path[path.Count - 1].spawnX == 0 && path[path.Count-1].spawnY == 0)
                                {
                                    flag = 3;
                                    break;
                                }
                                sx = path[path.Count - 1].spawnX;
                                sy = path[path.Count - 1].spawnY;
                                generator(sx, sy);
                                flag = 2;
                                break;

                            }
                            else
                            {
                                dir = (Directions)allowed[Random.Range(0, allowed.Count)];
                                continue;
                            }
                        }
                    }
                    
            }
            if (flag == 3)
            {
                isRun = true;
                break;
            }
            if (flag == 1 || flag == 2)
                continue;

        } while (true);
   
    }
    void wallgen()
    {
        for(float i=-0.625f;i<sizeX-0.125f; i+=0.25f)
        {
             Object.Instantiate(wallobj, new Vector3(i, sizeY - 0.375f), Quaternion.identity, t);
             Object.Instantiate(wallobj, new Vector3(i, -0.625f), Quaternion.identity, t);
        }
        for(float i=-0.625f;i<sizeY-0.125f;i+=0.25f)
        {
             Object.Instantiate(wallobj, new Vector3(-0.625f, i), Quaternion.identity, t);
             Object.Instantiate(wallobj, new Vector3(sizeX - 0.375f, i), Quaternion.identity, t);

        }
        for(int i=0; i<sizeX; i++)
        {
            for(int j=0; j<sizeY; j++)
            {
                 Object.Instantiate(wallobj, new Vector3(i - 0.375f, j - 0.375f), Quaternion.identity, t);
                 Object.Instantiate(wallobj, new Vector3(i - 0.375f, j + 0.375f), Quaternion.identity, t);
                 Object.Instantiate(wallobj, new Vector3(i + 0.375f, j - 0.375f), Quaternion.identity, t);
                 Object.Instantiate(wallobj, new Vector3(i + 0.375f, j + 0.375f), Quaternion.identity, t);
                if(grids[i][j].dir[0]==false)
                {
                     Object.Instantiate(wallobj, new Vector3(i + 0.375f, j - 0.125f), Quaternion.identity, t);
                     Object.Instantiate(wallobj, new Vector3(i + 0.375f, j + 0.125f), Quaternion.identity, t);
                }
                if (grids[i][j].dir[1] == false)
                {
                     Object.Instantiate(wallobj, new Vector3(i - 0.375f, j - 0.125f), Quaternion.identity, t);
                     Object.Instantiate(wallobj, new Vector3(i - 0.375f, j + 0.125f), Quaternion.identity, t);
                }
                if (grids[i][j].dir[2] == false)
                {
                     Object.Instantiate(wallobj, new Vector3(i - 0.125f, j + 0.375f), Quaternion.identity, t);
                     Object.Instantiate(wallobj, new Vector3(i + 0.125f, j + 0.375f), Quaternion.identity, t);
                }
                if (grids[i][j].dir[3] == false)
                {
                     Object.Instantiate(wallobj, new Vector3(i - 0.125f, j - 0.375f), Quaternion.identity, t);
                     Object.Instantiate(wallobj, new Vector3(i + 0.125f, j - 0.375f), Quaternion.identity, t);
                }
            }
        }
    }
    public void savedata(string name)
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("blue"))
        {
            blueTiles.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("yellow"))
        {
            yellowTiles.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("green"))
        {
            greenTiles.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("black"))
        {
            blackTiles.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("red"))
        {
            redTiles.Add(go.transform.GetSiblingIndex());
        }


        foreach(GameObject go in GameObject.FindGameObjectsWithTag("blueS"))
        {
            blueShifters.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("yellowS"))
        {
            yellowShifters.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("greenS"))
        {
            greenShifters.Add(go.transform.GetSiblingIndex());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("redS"))
        {
            redShifters.Add(go.transform.GetSiblingIndex());
        }

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        
        SaveSystem.Instance.Savegame(this,name);
    }
    public void loaddata(string name)
    {
        MazeData md = SaveSystem.Instance.Loadgame(name);
        sizeX = md.sizeX;
        sizeY = md.sizeY;
        for(int i = 0; i < sizeX; i++)
        {
            grids[i] = new Grids[sizeY];
        }
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                grids[i][j] = new Grids(i, j);
            }
        }
        for(int i=0;i<sizeX;i++)
        {
            for(int j=0;j<sizeY;j++)
            {
                for(int k=0;k<4;k++)
                {
                    grids[i][j].dir[k]= md.g[i,j][k];
                }
            }
        }
        blueTiles.Clear();
        yellowTiles.Clear();
        greenTiles.Clear();
        blackTiles.Clear();
        redTiles.Clear();
        wallgen();

        for(int i = 0;i<md.enemysx.Length;i++)
        {
            GameObject.Instantiate(enemyobj, new Vector2(md.enemysx[i],md.enemysy[i]), Quaternion.identity);
        }
        foreach(int i in md.blue_indices)
        {
            blueTiles.Add(i);
            t.GetChild(i).tag = "blue";
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;          
        }
        foreach(int i in md.yellow_indices)
        {
            yellowTiles.Add(i);
            t.GetChild(i).tag = "yellow";
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.yellow;          
        }
        foreach(int i in md.green_indices)
        {
            greenTiles.Add(i);
            t.GetChild(i).tag = "green";
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.green;          
        }
        foreach(int i in md.red_indices)
        {
            redTiles.Add(i);
            t.GetChild(i).tag = "red";
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;          
        }


        foreach(int i in md.blueS_indices)
        {
            blueShifters.Add(i);
            t.GetChild(i).tag = "blueS";
            t.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Blue");
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;          
        }
        foreach(int i in md.yellowS_indices)
        {
            yellowShifters.Add(i);
            t.GetChild(i).tag = "yellowS";
            t.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Yellow");
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.yellow;          
        }
        foreach(int i in md.greenS_indices)
        {
            greenShifters.Add(i);
            t.GetChild(i).tag = "greenS";
            t.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Green");
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.green;          
        }
        foreach(int i in md.redS_indices)
        {
            redShifters.Add(i);
            t.GetChild(i).tag = "redS";
            t.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Red");
            t.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;          
        }
    }
    public void refresh()
    {
        path.Clear();
        visited.Clear();
        isRun = false;
        foreach (Transform child in t) 
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            GameObject.Destroy(go.gameObject);
        }     
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("coin"))
        {
            GameObject.Destroy(go.gameObject);
        }     
    }
}

