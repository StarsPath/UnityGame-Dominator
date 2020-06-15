﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    public int sizeX = 0;
    public int sizeY = 0;
    public int seed = 0;
    public bool randomizeSeed = false;
    private float[,] map;
    private int[,] trueMap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize()
    {
        Vector2Int size = globalData.getSize();
        sizeX = Mathf.Clamp(size.x, 10, 1000);
        sizeY = Mathf.Clamp(size.y, 10, 1000);
        map = new float[sizeX, sizeY];
        trueMap = new int[sizeX, sizeY];
    }
    public void generate()
    {
        if (randomizeSeed)
            seed = (int)(Random.value * 1000);
        Random.InitState(seed);
        float scale = 8f;
        float initX = Random.Range(-1000f, 1000f);
        float initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = Mathf.PerlinNoise((initX + i + 1) / scale, (initY + j + 1) / scale) * 100;
            }
        }
        convertTrueMap();
    }
    public void convertTrueMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] > 80)   //Mountain
                {
                    trueMap[i, j] = 4;
                }
                else if (map[i, j] > 65)    //Hill
                {
                    trueMap[i, j] = 3;
                }
                else if (map[i, j] > 55)     //Forest
                {
                    trueMap[i, j] = 2;
                }
                else if (map[i, j] > 35)     //Ground
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
}