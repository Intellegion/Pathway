
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class Pathfinder
{

    List<GameObject> gl = new List<GameObject>();
    Grids start_pos, dest, curr_pos;
    GameObject go;
    public List<Grids> Find(Grids startpos, Grids destintation)
    {

        start_pos = startpos;
        dest = destintation;

        List<Grids> open_list = new List<Grids>();
        List<Grids> closed_list = new List<Grids>();
        List<Grids> children = new List<Grids>();


        foreach(GameObject gg in gl)
        {
            GameObject.Destroy(gg);
        }

        Grids curr_pos;


        int curr_index;

        start_pos.g = 0; 
        start_pos.h = Math.Abs(start_pos.spawnX - dest.spawnX) + Math.Abs(start_pos.spawnY - dest.spawnY);
        start_pos.f = start_pos.h;

        dest.h = 0;

        open_list.Add(start_pos);


        while(true)
        {
            curr_pos = open_list[0];
            curr_index = 0;
            for(int i =0; i<open_list.Count; i++)
            {
                if(open_list[i].f<=curr_pos.f && open_list[i].g>curr_pos.g)
                {
                    curr_pos = open_list[i];
                    curr_index = i;
                }
            }

            closed_list.Add(open_list[curr_index]);
            open_list.RemoveAt(curr_index);

            children.Clear();

            if(curr_pos.spawnX == dest.spawnX && curr_pos.spawnY == dest.spawnY)
            {
                List<Grids> path = new List<Grids>();
                int curr_g = dest.g;

                path.Add(curr_pos);
                while(true)
                {
                    children = Generate(curr_pos);
                    foreach(Grids child in children)
                    {
                        if(child.g==curr_g-1)
                        {
                            curr_pos = child;
                            curr_g--;
                            path.Add(curr_pos);
                            break;
                        }
                    }
                    if(curr_pos.spawnX==start_pos.spawnX && curr_pos.spawnY==start_pos.spawnY)
                    {
                        path.Reverse();
                        return path;
                    }
                }
            }

            children = Generate(curr_pos);
            

            foreach(Grids child in children)
            {
                if(closed_list.Contains(child))
                {
                   continue;
                }
                child.g = curr_pos.g + 1;
                child.h = Math.Abs(child.spawnX-dest.spawnX) + Math.Abs(child.spawnY-dest.spawnY);
                child.f = child.g + child.h;

                foreach(Grids open in open_list)
                {
                    if(child.Equals(open)&&child.g>open.g)
                        continue;
                }                   
                open_list.Add(child);
            }

        }
    }
    public List<Grids> Generate(Grids grids)
    {
        List<Grids> grid = new List<Grids>();
        if(MazeGen.grids[grids.spawnX][grids.spawnY].dir[0])
        {
            grid.Add(MazeGen.grids[grids.spawnX+1][grids.spawnY]);
        }
        if(MazeGen.grids[grids.spawnX][grids.spawnY].dir[1])
        {
            grid.Add(MazeGen.grids[grids.spawnX-1][grids.spawnY]);
        }
        if(MazeGen.grids[grids.spawnX][grids.spawnY].dir[2])
        {
            grid.Add(MazeGen.grids[grids.spawnX][grids.spawnY+1]);
        }
        if(MazeGen.grids[grids.spawnX][grids.spawnY].dir[3])
        {
            grid.Add(MazeGen.grids[grids.spawnX][grids.spawnY-1]);
        }
        return grid;
    }
}
