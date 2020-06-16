using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GlobalData : MonoBehaviour
{
    // Start is called before the first frame update
    public MapGenerator mapGen;

    public int mapSizeX;
    public int mapSizeY;
    public int[,] terrain;
    public Unit[,] units;
    public Structure[,] structures;

    public Tile[] terrainTiles;
    public Tile[] structureTiles;
    
    void Start()
    {
        terrain = new int[mapSizeX, mapSizeY];
        structures = new Structure[mapSizeX, mapSizeY];
        units = new Unit[mapSizeX, mapSizeY];
        mapGen.initialize();
        mapGen.generate();
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
    public int[,] getTerrain()
    {
        return this.terrain;
    }
    public void setUnits(Unit[,] units)
    {
        this.units = units;
    }
    public Unit[,] getUnits()
    {
        return this.units;
    }
    public void setStructure(Structure[,] structure)
    {
        this.structures = structure;
    }
    public Structure[,] getStructure()
    {
        return this.structures;
    }
    public Tile[] getTerrainTiles()
    {
        return terrainTiles;
    }
    public Tile[] getStructureTiles()
    {
        return structureTiles;
    }
}
