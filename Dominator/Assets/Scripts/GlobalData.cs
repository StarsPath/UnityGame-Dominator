using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Linq;

public class GlobalData : MonoBehaviour
{
    // Start is called before the first frame update
    public MapGenerator mapGen;
    public Display display;

    // Map Info
    public int mapSizeX;
    public int mapSizeY;
    public int[,] terrain;
    public GameObject[,] terrainGameObjects;

    // Resources
    public GameObject[] terrainTiles;
    public Sprite[] unitSprites;

    // AI components
    public Node[,] graph;
    public GameObject[,] units;
    private GameObject selectedUnit;

    
    void Start()
    {
        terrain = new int[mapSizeX, mapSizeY];
        terrainGameObjects = new GameObject[mapSizeX, mapSizeY];
        units = new GameObject[mapSizeX, mapSizeY];

        mapGen.initialize();
        mapGen.generate();
        graph = mapGen.generateGraph();
        display.initialize();
        display.drawTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pathFind()
    {
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
        List<Node> unvisted = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().pos.x,
                            selectedUnit.GetComponent<Unit>().pos.y];
        dist[source] = 0;
        prev[source] = null;

        Unit unitData = selectedUnit.GetComponent<Unit>();
        int newXmin = Mathf.Clamp(unitData.pos.x - unitData.mobility, 0, mapSizeX);
        int newYmin = Mathf.Clamp(unitData.pos.y - unitData.mobility, 0, mapSizeY);
        int newXmax = Mathf.Clamp(unitData.pos.x + unitData.mobility, 0, mapSizeX);
        int newYmax = Mathf.Clamp(unitData.pos.y + unitData.mobility, 0, mapSizeY);
        Vector2Int minPoint = new Vector2Int(newXmin, newYmin);
        Vector2Int maxPoint = new Vector2Int(newXmax, newYmax);

        for (int i = minPoint.x; i < maxPoint.x; i++)
        {
            for (int j = minPoint.y; j < maxPoint.y; j++)
            {
                //terrainGameObjects[i, j].GetComponent<ClickableTile>().setSelected();
                Node v = graph[i, j];
                if (v != source)
                {
                    dist[v] = Mathf.Infinity;
                    prev[v] = null;
                }
                unvisted.Add(v);
            }
        }
        while (unvisted.Count > 0)
        {
            Node u = unvisted.OrderBy(n => dist[n]).First();
            unvisted.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //Debug.Log(u.neighbours);
                float alt = dist[u] + u.distanceTo(v) + v.cost-1;
                if (alt > selectedUnit.GetComponent<Unit>().mobility)
                    continue;
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        showPath(dist.Where(n => n.Value < Mathf.Infinity).Select(n => n.Key).ToList());
    }
    public void showPath(List<Node> dist)
    {
        if (selectedUnit)
        {
            foreach (Node n in dist)
            {
                terrainGameObjects[n.x, n.y].GetComponent<ClickableTile>().setSelected();
            }
        }
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
        return terrain;
    }
    public GameObject[] getTerrainTiles()
    {
        return terrainTiles;
    }
    public Sprite[] getUnitSprites()
    {
        return unitSprites;
    }
    public void setSelectedUnit(GameObject unit)
    {
        selectedUnit = unit;
    }
}
