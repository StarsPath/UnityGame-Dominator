using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Display : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    public int[,] terrainData;
    public Unit[,] unitData;


    public Tilemap terrain;
    public Tilemap units;
    public Tilemap structures;

    public Tile[] terrainTiles;
    //public Tile[] unitTiles;
    public Tile[] structureTiles;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        initialize();
        drawTerrain();
        //drawUnits();
    }
    public void initialize()
    {
        terrainData = globalData.getTerrain();
        terrainTiles = globalData.getTerrainTiles();
        //unitTiles = globalData.getUnitTiles();
        structureTiles = globalData.getStructureTiles();
    }
    public void drawTerrain()
    {
        for (int i = 0; i < terrainData.GetLength(0); i++)
        {
            for (int j = 0; j < terrainData.GetLength(1); j++)
            {
                terrain.SetTile(new Vector3Int(i, j, 0), terrainTiles[terrainData[i, j]]);
            }
        }
    }
    public void drawUnits()
    {
        for (int i = 0; i < unitData.GetLength(0); i++)
        {
            for (int j = 0; j < unitData.GetLength(1); j++)
            {
                //units.SetTile(new Vector3Int(i, j, 0), unitTiles[(int)unitData[i, j].getType()]);
            }
        }
    }
}
