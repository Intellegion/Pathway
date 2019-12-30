using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemyMovement : StateMachine
{
    public States curr_state;
    public float speed = 1f;

    Pathfinder pf; 

    private int i = 0;

    private int startX, startY;

    private Grids nextpos, currpos;
    private Directions dir;
    private Directions opp_dir;
    private List<Directions> allow;

    protected override void StartGame()
    {
        t = this.transform;
        currpos = MazeGen.grids[(int)t.position.x][(int)t.position.y];
        allow = new List<Directions>();
        pf = new Pathfinder();
        Allowed_Directions();
        NextPos();
        Initialize();
        StartCoroutine(UpdatePosition());
    }  
    private void Allowed_Directions()
    {
        allow.Clear();
        if(currpos.dir[0])
        {
            allow.Add(Directions.East);
        }
        if(currpos.dir[2])
        {
            allow.Add(Directions.North);
        }
        if(currpos.dir[1])
        {
            allow.Add(Directions.West);
        }  
        if(currpos.dir[3])
        {
            allow.Add(Directions.South);
        }
        dir = allow[0];
        allow = allow.OrderBy(x => Random.value).ToList();
    }
    private void NextPos()
    {
        switch(dir)
            {
                case Directions.East:
                {
                    nextpos = MazeGen.grids[currpos.spawnX+1][currpos.spawnY];
                    opp_dir = Directions.West;
                    break;
                }
                case Directions.West:
                {
                    nextpos = MazeGen.grids[currpos.spawnX-1][currpos.spawnY];
                    opp_dir = Directions.East;
                    break;
                }
                case Directions.North:
                {
                    nextpos = MazeGen.grids[currpos.spawnX][currpos.spawnY+1];
                    opp_dir = Directions.South;
                    break;
                }
                case Directions.South:
                {
                    nextpos = MazeGen.grids[currpos.spawnX][currpos.spawnY-1];
                    opp_dir = Directions.North;
                    break;
                }
            }
    }
    protected override void Initialize()
    {
        foreach(Directions d in allow)
        {
            if(opp_dir.Equals(d))
            {
                continue;
            }
            dir = d;
            break;
        } 
    }
    protected override void FSM_Update()
    {
       
       
        
    }
    IEnumerator UpdatePosition()
    {
        while(true)
        { 
            float timeToStart = Time.time;
            while(Vector3.Distance(transform.position, new Vector2(nextpos.spawnX, nextpos.spawnY)) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector2(nextpos.spawnX, nextpos.spawnY), (Time.time - timeToStart )* speed );
                yield return null;
            }
            currpos = nextpos;  
            Allowed_Directions();
            Initialize();
            NextPos(); 
            continue;
        } 
    }    
}
