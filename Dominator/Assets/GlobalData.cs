using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GlobalData : MonoBehaviour
{
    // Start is called before the first frame update
    public int mapSizeX;
    public int mapSizeY;
    public int[,] terrain;
    public Unit[,] units;
    public Tilemap structures;

    public Tile[] terrainTiles;
    public Tile[] unitTiles;
    public Tile[] structureTiles;
    
    void Start()
    {
        
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
    public void setStructure(Tilemap structure)
    {
        this.structures = structure;
    }
    public Tilemap getStructure()
    {
        return this.structures;
    }
    public Tile[] getUnitTiles()
    {
        return unitTiles;
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
