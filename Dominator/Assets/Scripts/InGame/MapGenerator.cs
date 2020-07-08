using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    private int sizeX = 0;
    private int sizeY = 0;
    public int seed = 0;
    public bool randomizeSeed = false;
    private float[,] map;
    private int[,] trueMap;
    public GameObject[] terrainTiles;
    //private int[,] costMap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize()
    {
        terrainTiles = globalData.getTerrainTiles();
        Vector2Int size = globalData.getSize();
        sizeX = Mathf.Clamp(size.x, 10, 1000);
        sizeY = Mathf.Clamp(size.y, 10, 1000);
        map = new float[sizeX, sizeY];
        trueMap = new int[sizeX, sizeY];
        //costMap = new int[sizeX, sizeY];
    }
    public void generate()
    {
        if (randomizeSeed)
            seed = (int)(Random.value * 1000);
        Random.InitState(seed);
        float scale = 16f;
        float initX = Random.Range(-1000f, 1000f);
        float initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                map[i, j] = Mathf.PerlinNoise((initX + i + 1) / scale, (initY + j + 1) / scale) * 50;
            }
        }
        initX = Random.Range(-1000f, 1000f);
        initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                map[i, j] += Mathf.PerlinNoise((initX + i + 1) / scale, (initY + j + 1) / scale) * 50;
            }
        }
        convertTrueMap();
        drawTerrain();
    }
    public void convertTrueMap()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (map[i, j] > 80)   //Mountain
                {
                    trueMap[i, j] = 4;
                }
                else if (map[i, j] > 65)    //Hill
                {
                    trueMap[i, j] = 3;
                }
                else if (map[i, j] > 50)     //Forest
                {
                    trueMap[i, j] = 2;
                }
                else if (map[i, j] > 40)     //Ground
                {
                    trueMap[i, j] = 1;
                }
                else    //Water
                {
                    trueMap[i, j] = 0;
                }
            }
        }
        globalData.setTerrain(trueMap);
    }
    public int getCost(int tileID)
    {
        switch (tileID)
        {
            case 1:
                return 1;
            case 0:
            case 2:
            case 3:
                return 2;
            case 4:
                return 3;
        }
        return 1;
    }
    public Node[,] generateGraph()
    {
        Node[,] graph = new Node[sizeX, sizeY];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                graph[i, j] = new Node();
                /*if (i > 0)
                    graph[i, j].neighbours.Add(graph[i - 1, j]);
                if (i < sizeX-1)
                    graph[i, j].neighbours.Add(graph[i + 1, j]);
                if (j > 0)
                    graph[i, j].neighbours.Add(graph[i, j - 1]);
                if (j < sizeY-1)
                    graph[i, j].neighbours.Add(graph[i, j + 1]);*/
                graph[i, j].cost = getCost(trueMap[i,j]);
                graph[i, j].x = i;
                graph[i, j].y = j;            
            }
        }
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (i > 0)
                    graph[i, j].neighbours.Add(graph[i - 1, j]);
                if (i < sizeX - 1)
                    graph[i, j].neighbours.Add(graph[i + 1, j]);
                if (j > 0)
                    graph[i, j].neighbours.Add(graph[i, j - 1]);
                if (j < sizeY - 1)
                    graph[i, j].neighbours.Add(graph[i, j + 1]);
            }
        }
        return graph;
    }
    public void drawTerrain()
    {
        for (int i = 0; i < trueMap.GetLength(0); i++)
        {
            for (int j = 0; j < trueMap.GetLength(1); j++)
            {
                //terrain.SetTile(new Vector3Int(i, j, 0), terrainTiles[terrainData[i, j]]);
                GameObject terrainTile = Instantiate(terrainTiles[trueMap[i, j]], new Vector3(i, j, 1), Quaternion.identity, GameObject.Find("Map").transform);
                terrainTile.GetComponent<ClickableTile>().setData(i, j, null);
                globalData.terrainGameObjects[i, j] = terrainTile;
            }
        }
    }
}
