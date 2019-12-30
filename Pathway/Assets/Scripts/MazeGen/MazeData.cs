using System.Numerics;
[System.Serializable]
public class MazeData 
{
    public bool[,][] g = new bool[MazeGen.sizeX, MazeGen.sizeY][];
    public int[] blue_indices;
    public int[] yellow_indices;
    public int[] green_indices;
    public int[] black_indices;
    public int[] red_indices;
    public int[] blueS_indices;
    public int[] yellowS_indices;
    public int[] greenS_indices;
    public int[] redS_indices;

    public int[] enemysx;
    public int[] enemysy;
    public MazeData(MazeGen mg)
    {
        blue_indices = new int[mg.blueTiles.Count];
        yellow_indices = new int[mg.yellowTiles.Count];
        green_indices = new int[mg.greenTiles.Count];
        black_indices = new int[mg.blackTiles.Count];
        red_indices = new int[mg.redTiles.Count];

        blueS_indices = new int[mg.blueShifters.Count];
        yellowS_indices = new int[mg.yellowShifters.Count];
        greenS_indices = new int[mg.greenShifters.Count];
        redS_indices = new int[mg.redShifters.Count];

        enemysx = new int[mg.enemies.Length];
        enemysy = new int[mg.enemies.Length];

        for(int i = 0; i<MazeGen.sizeX; i++)
        {
            for(int j = 0; j<MazeGen.sizeY; j++)
            {
                g[i,j] = new bool[4];
            }
        }
        for(int i=0;i<MazeGen.sizeX;i++)
        {
            for(int j=0;j<MazeGen.sizeY;j++)
            {
                for(int k=0;k<4;k++)
                {
                    g[i,j][k]=MazeGen.grids[i][j].dir[k];
                }
            }
        }
        for(int i =0;i<mg.blueTiles.Count;i++)
        {
            blue_indices[i]=mg.blueTiles[i];
        }
        for(int i =0;i<mg.yellowTiles.Count;i++)
        {
            yellow_indices[i]=mg.yellowTiles[i];
        }
        for(int i =0;i<mg.greenTiles.Count;i++)
        {
            green_indices[i]=mg.greenTiles[i];
        }
        for(int i =0;i<mg.blackTiles.Count;i++)
        {
            black_indices[i]=mg.blackTiles[i];
        }
        for(int i =0;i<mg.redTiles.Count;i++)
        {
            red_indices[i]=mg.redTiles[i];
        }


        for(int i =0;i<mg.blueShifters.Count;i++)
        {
            blueS_indices[i]=mg.blueShifters[i];
        }
        for(int i =0;i<mg.yellowShifters.Count;i++)
        {
            yellowS_indices[i]=mg.yellowShifters[i];
        }
        for(int i =0;i<mg.greenShifters.Count;i++)
        {
            greenS_indices[i]=mg.greenShifters[i];
        }
        for(int i =0;i<mg.redShifters.Count;i++)
        {
            redS_indices[i]=mg.redShifters[i];
        }     

        for(int i =0; i<mg.enemies.Length;i++)
        {
            enemysx[i] = (int)mg.enemies[i].transform.position.x;
            enemysy[i] = (int)mg.enemies[i].transform.position.y;
        }
    }
}
