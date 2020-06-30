using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Display : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    public int[,] terrainData;

    public GameObject[] terrainTiles;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize()
    {
        terrainData = globalData.getTerrain();
        terrainTiles = globalData.getTerrainTiles();
    }
    public void drawTerrain()
    {
        for (int i = 0; i < terrainData.GetLength(0); i++)
        {
            for (int j = 0; j < terrainData.GetLength(1); j++)
            {
                //terrain.SetTile(new Vector3Int(i, j, 0), terrainTiles[terrainData[i, j]]);
                GameObject terrainTile = Instantiate(terrainTiles[terrainData[i, j]], new Vector3(i, j, 1), Quaternion.identity, GameObject.Find("Map").transform);
                terrainTile.GetComponent<ClickableTile>().setData(i, j, null, false);
            }
        }
    }
}
