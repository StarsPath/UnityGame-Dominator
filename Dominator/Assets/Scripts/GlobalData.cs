using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GlobalData : MonoBehaviour
{
    // Start is called before the first frame update
    public MapGenerator mapGen;
    public Display display;

    public int mapSizeX;
    public int mapSizeY;
    public int[,] terrain;
    public int[,] terrainCost;

    public Tile[] terrainTiles;
    public Sprite[] unitSprites;
    
    void Start()
    {
        terrain = new int[mapSizeX, mapSizeY];
        mapGen.initialize();
        mapGen.generate();
        display.initialize();
        display.drawTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2Int getSize()
    {
        Vector2Int size = new Vector2Int(this.mapSizeX, this.mapSizeY);
        return size;
    }
    public void setTerrain(int[,] terrain)
    {
        this.terrain = terrain;
    }
    public void setTerrainCost(int[,] cost)
    {
        this.terrainCost = cost;
    }
    public int[,] getTerrain()
    {
        return terrain;
    }
    public int[,] getTerrainCost()
    {
        return terrainCost;
    }
    public Tile[] getTerrainTiles()
    {
        return terrainTiles;
    }
    public Sprite[] getUnitSprites()
    {
        return unitSprites;
    }
}
