using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

public class MapEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public string path;
    [System.Serializable]
    public class MapData
    {
        public string fileName;
        public int mapX;
        public int mapY;
        public float scale;
        public int seed;
        public bool randomizeSeed;
        public float[,] map;
        public int[,] trueMap;
        //public GameObject[,] terrainGameObjects;

        public float mountainLevel;
        public float hillsLevel;
        public float forestLevel;
        public float groundLevel;
        public MapData()
        {
            mapX = 10;
            mapY = 10;
            scale = 3f;
            seed = 0;

            randomizeSeed = false;
            mountainLevel = 80;
            hillsLevel = 65;
            forestLevel = 50;
            groundLevel = 40;
        }
        public void initMap()
        {
            map = new float[mapX, mapY];
            trueMap = new int[mapX, mapY];
        }
    }

    MapData mapData;

    
    /*public string fileName;

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
    public float groundLevel = 40;*/

    public TMP_InputField fileNameField;

    public GameObject[] terrainTiles;
    //public GameObject[,] terrainGameObjects;

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
        mapData = new MapData();
        path = Application.persistentDataPath + "/map/";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initializeMap()
    {
        //mapData.map = new float[mapData.mapX, mapData.mapY];
        //mapData.trueMap = new int[mapData.mapX, mapData.mapY];
        //mapData.terrainGameObjects = new GameObject[mapData.mapX, mapData.mapY];
    }
    public void generate()
    {
        //initializeMap();
        //Debug.Log(mapData.map[0,0]);
        //Debug.Log(mapData.mapX);
        //Debug.Log(mapData.mapY);
        mapData.initMap();

        if (mapData.randomizeSeed)
        {
            mapData.seed = (int)(Random.value * 100000);
            mapData.fileName = mapData.seed.ToString();
        }
        Random.InitState(mapData.seed);
        RandomSeedFeedback();
        float initX = Random.Range(-1000f, 1000f);
        float initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < mapData.mapX; i++)
        {
            for (int j = 0; j < mapData.mapY; j++)
            {
                mapData.map[i, j] = Mathf.PerlinNoise((initX + i + 1) / mapData.scale, (initY + j + 1) / mapData.scale) * 50;
            }
        }
        initX = Random.Range(-1000f, 1000f);
        initY = Random.Range(-1000f, 1000f);
        for (int i = 0; i < mapData.mapX; i++)
        {
            for (int j = 0; j < mapData.mapY; j++)
            {
                mapData.map[i, j] += Mathf.PerlinNoise((initX + i + 1) / mapData.scale, (initY + j + 1) / mapData.scale) * 50;
            }
        }
        refreshMap();
    }
    public void refreshMap()
    {
        convertTrueMap();
        drawTerrain();
    }
    public void convertTrueMap()
    {
        for (int i = 0; i < mapData.mapX; i++)
        {
            for (int j = 0; j < mapData.mapY; j++)
            {
                if (mapData.map[i, j] > mapData.mountainLevel)   //Mountain
                {
                    mapData.trueMap[i, j] = 4;
                }
                else if (mapData.map[i, j] > mapData.hillsLevel)    //Hill
                {
                    mapData.trueMap[i, j] = 3;
                }
                else if (mapData.map[i, j] > mapData.forestLevel)     //Forest
                {
                    mapData.trueMap[i, j] = 2;
                }
                else if (mapData.map[i, j] > mapData.groundLevel)      //Ground
                {
                    mapData.trueMap[i, j] = 1;
                }
                else        //Water
                    mapData.trueMap[i, j] = 0;
            }
        }
    }
    public void drawTerrain()
    {
        resetMap();
        for (int i = 0; i < mapData.trueMap.GetLength(0); i++)
        {
            for (int j = 0; j < mapData.trueMap.GetLength(1); j++)
            {
                GameObject terrainTile = Instantiate(terrainTiles[mapData.trueMap[i, j]], new Vector3(i, j, 1), Quaternion.identity, GameObject.Find("Map").transform);
                terrainTile.GetComponent<ClickableTile>().setData(i, j, null);
                //mapData.terrainGameObjects[i, j] = terrainTile;
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
        path = Application.persistentDataPath + "/map/";

        if (!File.Exists(path))
            Directory.CreateDirectory(Application.persistentDataPath + "/map/");

        string targetPath = EditorUtility.SaveFilePanel("Open Map File", path, mapData.seed.ToString(), "map");

        FileStream stream = new FileStream(targetPath + ".map", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();


        formatter.Serialize(stream, mapData);
        stream.Close();

    }
    public void load()
    {
        string targetPath = EditorUtility.OpenFilePanel("Open Map File", path, "map");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(targetPath, FileMode.Open);

        mapData = formatter.Deserialize(stream) as MapData;

        stream.Close();
        drawTerrain();
        updateAllField();
    }
    public void updateAllField()
    {
        fileNameField.text = mapData.fileName;
        MapXSlider.value = mapData.mapX;
        MapYSlider.value = mapData.mapY;
        ScaleSlider.value = mapData.scale;

        MapXField.text = mapData.mapX.ToString();
        MapYField.text = mapData.mapY.ToString();
        ScaleField.text = mapData.scale.ToString();

        MountainLevelSlider.value = mapData.mountainLevel;
        HillsLevelSlider.value = mapData.hillsLevel;
        ForestLevelSlider.value = mapData.forestLevel;
        GroundLevelSlider.value = mapData.groundLevel;

        MountainLevelField.text = mapData.mountainLevel.ToString();
        HillsLevelField.text = mapData.hillsLevel.ToString();
        ForestLevelField.text = mapData.forestLevel.ToString();
        GroundLevelField.text = mapData.groundLevel.ToString();

        MapSeedField.text = mapData.seed.ToString();
        RandomSeedToggle.isOn = mapData.randomizeSeed;
        
    }
    public void fileNameChange()
    {
        mapData.fileName = fileNameField.text;
    }
    public void sliderChange()
    {
        int sliderXValue = (int)Mathf.Clamp(MapXSlider.value, 10, 100);
        int sliderYValue = (int)Mathf.Clamp(MapYSlider.value, 10, 100);
        int sliderScaleValue = (int)Mathf.Clamp(ScaleSlider.value, 3, 16);
        MapXField.text = sliderXValue.ToString();
        MapYField.text = sliderYValue.ToString();
        ScaleField.text = sliderScaleValue.ToString();
        mapData.mapX = sliderXValue;
        mapData.mapY = sliderYValue;
        mapData.scale = sliderScaleValue;
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

        mapData.mapX = sliderXValue;
        mapData.mapY = sliderYValue;
        mapData.scale = sliderScaleValue;
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

        mapData.mountainLevel = mountainValue;
        mapData.hillsLevel = hillsValue;
        mapData.forestLevel = forestValue;
        mapData.groundLevel = groundValue;

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

        mapData.mountainLevel = mountainValue;
        mapData.hillsLevel = hillsValue;
        mapData.forestLevel = forestValue;
        mapData.groundLevel = groundValue;
    }
    public void setDefault()
    {
        mapData.mountainLevel = 80;
        mapData.hillsLevel = 65;
        mapData.forestLevel = 50;
        mapData.groundLevel = 40;

        MountainLevelSlider.value = mapData.mountainLevel;
        HillsLevelSlider.value = mapData.hillsLevel;
        ForestLevelSlider.value = mapData.forestLevel;
        GroundLevelSlider.value = mapData.groundLevel;

        MountainLevelField.text = mapData.mountainLevel.ToString();
        HillsLevelField.text = mapData.hillsLevel.ToString();
        ForestLevelField.text = mapData.forestLevel.ToString();
        GroundLevelField.text = mapData.groundLevel.ToString();
    }
    public void setRanomize()
    {
        if (string.IsNullOrEmpty(MapSeedField.text))
        {
            mapData.seed = 0;
            MapSeedField.text = "0";
        }
        else
            mapData.seed = int.Parse(MapSeedField.text);
        mapData.randomizeSeed = RandomSeedToggle.isOn;
    }
    public void RandomSeedFeedback()
    {
        MapSeedField.text = mapData.seed.ToString();
    }
    public Vector2Int getSize()
    {
        if (mapData == null)
            return new Vector2Int(0, 0);
        Debug.Log(mapData.mapX);
        Debug.Log(mapData.mapY);
        return new Vector2Int(mapData.mapX, mapData.mapY);
    }
}
