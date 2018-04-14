using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    public int height;
    public int width;
    public int mapFillPercent;
    
    public bool randomSeed;
    public string seed;

    public int[,] mapArray;

    public GameObject dungeonBrick, dungeonBrick2, dungeonBrick3, dungeonBrick4;
    public GameObject dungeonWall;

    public GameObject turret1, turret2, turret3, turret4;

    public GameObject gem1, gem2, gem3, gem4, gem5;

    public GameObject player, gate;

    public List<Vector3> viableGemSpawnList;

    // Use this for initialization
    void Start()
    {
        mapArray = new int[width, height];
        RandomFillMap();

        SmoothMap();
        SmoothMap();
        SmoothMap();
        SmoothMap();
        SmoothMap();

        createBorders();

        createWalls();

        if (!spawnPlayer())
        {
            //COULDN'T FIND A SPAWN - REBUILD MAP AND TRY AGAIN
            Debug.Log("Failed to find player spawn point, reseeding map and trying again");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        populateGemList();
        if (viableGemSpawnList.Count < 5)
        {
            Debug.Log("Failed to find sufficient gem spawns, reloading map and trying again");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        spawnGemsAndTurrets();



    }

    bool spawnPlayer()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               if (mapArray[x,y] == 0)
                {
                    if (tileOnFloor(x, y)){
                        //spawn player here
                        Vector3 pos = new Vector3((-width / 2 + x + 0.5f)*0.1f, (-height / 2 + y + 0.5f)*0.1f, 0);
                        player.transform.position = pos;
                        gate.transform.position = pos+ new Vector3(0.0f,0.06f,0.0f);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void populateGemList()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapArray[x, y] == 0)
                {
                    if (tileOnFloor(x, y))
                    {
                        //spawn player here
                        Vector3 pos = new Vector3((-width / 2 + x + 0.5f) * 0.1f, ((-height / 2 + y + 0.5f) * 0.1f)+0.06f, 0);
                        viableGemSpawnList.Add(pos);                      
                    }
                }
            }
        }
    }

    void spawnGemsAndTurrets() {

        double spawns = viableGemSpawnList.Count;
        double numberOfGems = 5;
        double separation = spawns / (numberOfGems *2);
        double count = System.Math.Floor(separation);
        int countInt = System.Convert.ToInt32(count);

        Vector3 t1pos = new Vector3(viableGemSpawnList[4 * countInt].x, viableGemSpawnList[4 * countInt].y - 0.1f);
        turret1.transform.position = t1pos;
        Vector3 t2pos = new Vector3(viableGemSpawnList[7 * countInt].x, viableGemSpawnList[7 * countInt].y - 0.1f);
        turret2.transform.position = t2pos;
        Vector3 t3pos = new Vector3(viableGemSpawnList[2 * countInt].x, viableGemSpawnList[2 * countInt].y - 0.1f);
        turret3.transform.position = t3pos;
        Vector3 t4pos = new Vector3(viableGemSpawnList[9 * countInt].x, viableGemSpawnList[9 * countInt].y - 0.1f);
        turret4.transform.position = t4pos;

        gem1.transform.position = viableGemSpawnList[3 * countInt];
        gem2.transform.position = viableGemSpawnList[5 * countInt];
        gem3.transform.position = viableGemSpawnList[6 * countInt];
        gem4.transform.position = viableGemSpawnList[8 * countInt];
        int lastSpawn = 10 * countInt;
        if((lastSpawn) >= viableGemSpawnList.Count)
        {
            lastSpawn = viableGemSpawnList.Count-1;
        }
        gem5.transform.position = viableGemSpawnList[lastSpawn];
    }

    bool tileOnFloor(int xCoord, int yCoord)
    {
        if (mapArray[xCoord, yCoord] > 0)
        {
            //tile isn't ON floor, tile IS floor!
            return false;
        }

        if(mapArray[xCoord + 1, yCoord] == 0 && mapArray[xCoord - 1, yCoord] == 0 && mapArray[xCoord + 1, yCoord -1] == 1 
            && mapArray[xCoord - 1, yCoord -1] == 1 && mapArray[xCoord, yCoord - 1] == 1)
        {
            //small area of floor found
            //checking that there is at least a 9x9 area to spawn in
            if(mapArray[xCoord - 1, yCoord + 1] == 0 && mapArray[xCoord + 1, yCoord + 1] == 0 && mapArray[xCoord, yCoord + 1] == 0 &&
            mapArray[xCoord - 1, yCoord + 2] == 0 && mapArray[xCoord + 1, yCoord + 2] == 0 && mapArray[xCoord, yCoord + 2] == 0){
                return true;
            }
        }

        return false;
    }


    void RandomFillMap()
    {
        if (randomSeed)
        {
            seed = System.DateTime.Now.ToString();
        }

        System.Random seededRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //set up solid walls for all maps
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    mapArray[x, y] = 1;
                }
                else
                {
                    if (seededRandom.Next(0, 100) < mapFillPercent)
                    {
                        mapArray[x, y] = 1;
                    }
                    else mapArray[x, y] = 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWalls(x, y);
                //Debug.Log("surrounding tiles = " + neighbourWallTiles);
                if (neighbourWallTiles > 4)
                    mapArray[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    mapArray[x, y] = 0;

            }
        }
    }

    private void createBorders()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               if (x==0 || y==0 || y==height-1 || x == width - 1)
                {
                    mapArray[x, y] = 2;
                }
            }
        }
    }

    private void createWalls()
    {
        if (mapArray != null)
        {
            System.Random rand = new System.Random();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                   // if (mapArray[x, y] == 1) Gizmos.color = Color.black;
                   // else Gizmos.color = Color.white;

                    Vector3 pos = new Vector3((-width / 2 + x + 0.5f)*0.1f, (-height / 2 + y + 0.5f)*0.1f, 0);
                    

                    // Gizmos.DrawCube(pos, Vector3.one);
                    if (mapArray[x, y] == 1)
                    {
                        int t = rand.Next(1, 5);
                        if(t == 1)
                        {
                            GameObject.Instantiate(dungeonBrick, pos, dungeonBrick.transform.rotation);
                        }
                        if(t == 2)
                        {
                            GameObject.Instantiate(dungeonBrick2, pos, dungeonBrick.transform.rotation);
                        }
                        if(t == 3)
                        {
                            GameObject.Instantiate(dungeonBrick3, pos, dungeonBrick.transform.rotation);
                        }
                        if(t == 4)
                        {
                            GameObject.Instantiate(dungeonBrick4, pos, dungeonBrick.transform.rotation);
                        }
                       // GameObject.Instantiate(dungeonBrick, pos, dungeonBrick.transform.rotation);
                    } else if (mapArray[x, y] == 2)
                    {
                        GameObject.Instantiate(dungeonWall, pos, dungeonWall.transform.rotation);
                    }
                }
            }
        }
    } 

    int GetSurroundingWalls(int xloc, int yloc)
    {
        int wallCount = 0;
        for (int x = xloc - 1; x <= xloc + 1; x++)
        {
            for (int y = yloc - 1; y <= yloc + 1; y++)
            {
                if (x >= 0 && y >= 0 && x < width && y < height)
                {
                    if (x != xloc || y != yloc)
                    {
                        wallCount += mapArray[x, y];
                    }
                }
                else wallCount++;
            }
        }
        return wallCount;
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            SmoothMap();
        }*/

        //Debug.Log(viableGemSpawnList.Count);
    }
}
