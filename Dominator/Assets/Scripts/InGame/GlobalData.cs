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
    public List<Node> selectedTiles;

    
    void Start()
    {
        terrain = new int[mapSizeX, mapSizeY];
        terrainGameObjects = new GameObject[mapSizeX, mapSizeY];
        units = new GameObject[mapSizeX, mapSizeY];

        mapGen.initialize();
        mapGen.generate();
        graph = mapGen.generateGraph();
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
                Debug.Log(units[1, 1]);
                float alt = dist[u] + u.distanceTo(v) + v.cost-1;
                if (alt > selectedUnit.GetComponent<Unit>().mobility)
                    continue;
                if (v.x < newXmax && v.x >= newXmin && v.y < newYmax && v.y >= newYmin)
                {
                    if(units[v.x, v.y] != null)
                    {
                        //alt = Mathf.Infinity;
                        continue;
                    }
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
            }
        }
        selectedTiles = dist.Where(n => n.Value < Mathf.Infinity).Select(n => n.Key).ToList();
        showPath(selectedTiles);
    }
    public void PathFindSkip()
    {
        Unit unitData = selectedUnit.GetComponent<Unit>();
        int newXmin = Mathf.Clamp(unitData.pos.x - unitData.range-1, 0, mapSizeX);
        int newYmin = Mathf.Clamp(unitData.pos.y - unitData.range-1, 0, mapSizeY);
        int newXmax = Mathf.Clamp(unitData.pos.x + unitData.range+1, 0, mapSizeX);
        int newYmax = Mathf.Clamp(unitData.pos.y + unitData.range+1, 0, mapSizeY);
        Vector2Int minPoint = new Vector2Int(newXmin, newYmin);
        Vector2Int maxPoint = new Vector2Int(newXmax, newYmax);


        for (int i = minPoint.x; i < maxPoint.x; i++)
        {
            for (int j = minPoint.y; j < maxPoint.y; j++)
            {
                if(((int)(Mathf.Abs(i - unitData.pos.x)) + (int)(Mathf.Abs(j - unitData.pos.y))) <= unitData.range)
                {
                    selectedTiles.Add(graph[i, j]);
                }
            }
        }
        showPath(selectedTiles);
    }
    public void showPath(List<Node> dist)
    {
        Unit unitData = selectedUnit.GetComponent<Unit>();
        if (selectedUnit)
        {
            switch (unitData.status)
            {
                case Unit.Status.Unselected:
                    foreach (Node n in dist)
                    {
                        terrainGameObjects[n.x, n.y].GetComponent<ClickableTile>().setSelected();
                    }
                    break;
                case Unit.Status.Moved:
                    foreach (Node n in dist)
                    {
                        terrainGameObjects[n.x, n.y].GetComponent<ClickableTile>().setRanged();
                    }
                    break;
            }
            
        }
    }
    public void resetSeclection()
    {
        if (selectedUnit)
        {
            selectedUnit = null;
            foreach (Node n in selectedTiles)
            {
                terrainGameObjects[n.x, n.y].GetComponent<ClickableTile>().unselected();
            }
            selectedTiles.RemoveAll(n => true);
        }
    }
    public void moveUnitTo(int x, int y)
    {
        Unit unitData = selectedUnit.GetComponent<Unit>();
        units[unitData.pos.x, unitData.pos.y] = null;
        units[x, y] = selectedUnit;
        selectedUnit.GetComponent<Unit>().moveTo(x, y);
        resetSeclection();
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
    public bool hasSelectedUnit()
    {
        return selectedUnit == null ? false : true;
    }
}
