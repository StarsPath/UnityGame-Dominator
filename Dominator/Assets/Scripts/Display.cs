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
    public Structure[,] structureData;


    public Tilemap terrain;
    //public Tilemap units;
    public Tilemap structures;

    public Tile[] terrainTiles;
    public GameObject[] unitObj;
    public GameObject[] structureObj;
    public Tile[] structureTiles;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        initialize();
        drawTerrain();
        //drawStructures();
        //drawUnits();
    }
    public void initialize()
    {
        terrainData = globalData.getTerrain();
        unitData = globalData.getUnits();
        structureData = globalData.getStructure();


        terrainTiles = globalData.getTerrainTiles();
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
                if(unitData[i,j] != null)
                {
                    Instantiate(unitObj[(int)unitData[i, j].getType()], new Vector3Int(i, j, 0), Quaternion.identity, GameObject.Find("Units").transform);
                }
            }
        }
    }
    public void drawStructures()
    {
        for (int i = 0; i < structureData.GetLength(0); i++)
        {
            for (int j = 0; j < structureData.GetLength(1); j++)
            {
                if(structureData[i,j] != null)
                {
                    if (structureData[i, j].isResource())
                        structures.SetTile(new Vector3Int(i, j, 0), structureTiles[(int)structureData[i, j].getType()]);
                    else if (!structureData[i, j].isResource())
                        Instantiate(structureObj[(int)structureData[i, j].getType() - 3], new Vector3Int(i, j, 0), Quaternion.identity, GameObject.Find("Units").transform);
                }
            }
        }
    }
}
