using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    //public Unit[,] unitData;

    public int x;
    public int y;
    //public Unit.Type type;
    public bool enemy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void initialize()
    {
        unitData = globalData.getUnits();
    }
    public void spawnUnit()
    {
        Unit unit = new Unit(type, new Vector2Int(x, y), enemy);
        unitData[x, y] = unit;
    }
    public void spawnUnit(int x, int y, Unit.Type type, bool enemy)
    {
        Unit unit = new Unit(type, new Vector2Int(x, y), enemy);
        unitData[x, y] = unit;
    } */
}
