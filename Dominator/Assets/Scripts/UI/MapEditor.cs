using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public int mapX = 10;
    public int mapY = 10;
    public float scale = 3f;
    public int seed = 0;
    public bool randomizeSeed = false;
    private float[,] map;
    private int[,] trueMap;

    public float mountainLevel = 80;
    public float hillsLevel = 65;
    public float forestLevel = 50;
    public float groundLevel = 40;


    public GameObject[] terrainTiles;
    public GameObject[,] terrainGameObjects;

    public TMP_InputField MapXField;
    public TMP_InputField MapYField;
    public TMP_InputField ScaleField;

    public Slider MapXSlider;
    public Slider MapYSlider;
    public Slider ScaleSlider;

    public TMP_InputField MapSeedField;
    public Toggle RandomSeedToggle;

    public TMP_InputField MountainLevelField;
    public TMP_InputField HillsLevelField;
    public TMP_InputField ForestLevelField;
    public TMP_InputField GroundLevelField;

    public Slider MountainLevelSlider;
    public Slider HillsLevelSlider;
    public Slider ForestLevelSlider;
    public Slider GroundLevelSlider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initializeMap()
    {
        map = new float[mapX, mapY];
        trueMap = new int[mapX, mapY];
        terrainGameObjects = new GameObject[mapX, mapY];
    }
    public void generate()
    {
        initializeMap();
        if (randomizeSeed)
            seed = (int)(Random.value * 100000);
        Random.InitState(seed);
        RandomSeedFeedback();
        float initX = Random.Range(-1000f, 1000f);
        float initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < mapX; i++)
        {
            for (int j = 0; j < mapY; j++)
            {
                map[i, j] = Mathf.PerlinNoise((initX + i + 1) / scale, (initY + j + 1) / scale) * 50;
            }
        }
        initX = Random.Range(-1000f, 1000f);
        initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < mapX; i++)
        {
            for (int j = 0; j < mapY; j++)
            {
                map[i, j] += Mathf.PerlinNoise((initX + i + 1) / scale, (initY + j + 1) / scale) * 50;
            }
        }
        refreshMap();
    }
    public void refreshMap()
    {
        resetMap();
        convertTrueMap();
        drawTerrain();
    }
    public void convertTrueMap()
    {
        for (int i = 0; i < mapX; i++)
        {
            for (int j = 0; j < mapY; j++)
            {
                if (map[i, j] > mountainLevel)   //Mountain
                {
                    trueMap[i, j] = 4;
                }
                else if (map[i, j] > hillsLevel)    //Hill
                {
                    trueMap[i, j] = 3;
                }
                else if (map[i, j] > forestLevel)     //Forest
                {
                    trueMap[i, j] = 2;
                }
                else if (map[i, j] > groundLevel)      //Ground
                {
                    trueMap[i, j] = 1;
                }
                else        //Water
                    trueMap[i, j] = 0;
            }
        }
    }
    public void drawTerrain()
    {
        for (int i = 0; i < trueMap.GetLength(0); i++)
        {
            for (int j = 0; j < trueMap.GetLength(1); j++)
            {
                GameObject terrainTile = Instantiate(terrainTiles[trueMap[i, j]], new Vector3(i, j, 1), Quaternion.identity, GameObject.Find("Map").transform);
                terrainTile.GetComponent<ClickableTile>().setData(i, j, null);
                terrainGameObjects[i, j] = terrainTile;
            }
        }
    }
    public void resetMap()
    {
        foreach(Transform child in GameObject.Find("Map").transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void save()
    {

    }
    public void load()
    {

    }
    public void sliderChange()
    {
        int sliderXValue = (int)Mathf.Clamp(MapXSlider.value, 10, 100);
        int sliderYValue = (int)Mathf.Clamp(MapYSlider.value, 10, 100);
        int sliderScaleValue = (int)Mathf.Clamp(ScaleSlider.value, 3, 16);
        MapXField.text = sliderXValue.ToString();
        MapYField.text = sliderYValue.ToString();
        ScaleField.text = sliderScaleValue.ToString();
        mapX = sliderXValue;
        mapY = sliderYValue;
        scale = sliderScaleValue;
    }
    public void fieldChange()
    {
        int sliderXValue = (int)Mathf.Clamp(int.Parse(MapXField.text), 10, 100);
        int sliderYValue = (int)Mathf.Clamp(int.Parse(MapYField.text), 10, 100);
        int sliderScaleValue = (int)Mathf.Clamp(int.Parse(ScaleField.text), 3, 16);
        MapXSlider.value = sliderXValue;
        MapYSlider.value = sliderYValue;
        ScaleSlider.value = sliderScaleValue;
        
        MapXField.text = sliderXValue.ToString();
        MapYField.text = sliderYValue.ToString();
        ScaleField.text = sliderScaleValue.ToString();

        mapX = sliderXValue;
        mapY = sliderYValue;
        scale = sliderScaleValue;
    }
    public void levelSliderChange()
    {
        float mountainValue = Mathf.Clamp(MountainLevelSlider.value, 0, 100);
        float hillsValue = Mathf.Clamp(HillsLevelSlider.value, 0, 100);
        float forestValue = Mathf.Clamp(ForestLevelSlider.value, 0, 100);
        float groundValue = Mathf.Clamp(GroundLevelSlider.value, 0, 100);

        MountainLevelField.text = mountainValue.ToString();
        HillsLevelField.text = hillsValue.ToString();
        ForestLevelField.text = forestValue.ToString();
        GroundLevelField.text = groundValue.ToString();

        mountainLevel = mountainValue;
        hillsLevel = hillsValue;
        forestLevel = forestValue;
        groundLevel = groundValue;

    }
    public void levelFieldChange()
    {
        float mountainValue = Mathf.Clamp(float.Parse(MountainLevelField.text), 0, 100);
        float hillsValue = Mathf.Clamp(float.Parse(HillsLevelField.text), 0, 100);
        float forestValue = Mathf.Clamp(float.Parse(ForestLevelField.text), 0, 100);
        float groundValue = Mathf.Clamp(float.Parse(GroundLevelField.text), 0, 100);

        MountainLevelSlider.value = mountainValue;
        HillsLevelSlider.value = hillsValue;
        ForestLevelSlider.value = forestValue;
        GroundLevelSlider.value = groundValue;

        mountainLevel = mountainValue;
        hillsLevel = hillsValue;
        forestLevel = forestValue;
        groundLevel = groundValue;
    }
    public void setDefault()
    {
        mountainLevel = 80;
        hillsLevel = 65;
        forestLevel = 50;
        groundLevel = 40;

        MountainLevelSlider.value = mountainLevel;
        HillsLevelSlider.value = hillsLevel;
        ForestLevelSlider.value = forestLevel;
        GroundLevelSlider.value = groundLevel;

        MountainLevelField.text = mountainLevel.ToString();
        HillsLevelField.text = hillsLevel.ToString();
        ForestLevelField.text = forestLevel.ToString();
        GroundLevelField.text = groundLevel.ToString();
    }
    public void setRanomize()
    {
        if (string.IsNullOrEmpty(MapSeedField.text))
        {
            seed = 0;
            MapSeedField.text = "0";
        }
        else
            seed = int.Parse(MapSeedField.text);
        randomizeSeed = RandomSeedToggle.isOn;
    }
    public void RandomSeedFeedback()
    {
        MapSeedField.text = seed.ToString();
    }
    public Vector2Int getSize()
    {
        return new Vector2Int(mapX, mapY);
    }
}
