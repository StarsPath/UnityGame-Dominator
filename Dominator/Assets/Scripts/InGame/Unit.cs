﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Status
    {
        Unselected,
        Selected,
        Moved,
        Attacked,
        Ended
    }

    public GlobalData globalData;
    public UnitScriptable unit;
    public Image healthBar;
    private SpriteRenderer sp;

    public UnitScriptable.Type type;
    public Vector2Int pos;
    public int HPMax;
    public int HPCurrent;
    public int ATK;
    public int cost;
    public int mobility;
    public int range;
    public int constructTime;

    public bool enemy;

    public Status status;

    public int[,] movementOverlay;
    public int[,] attackOverlay;

    //public Node[,] graph;

    void Start()
    {
        globalData = GameObject.Find("GameManager").GetComponent<GlobalData>();
        initialize();
        drawUnit();
        globalData.units[pos.x, pos.y] = gameObject;
        //graph = globalData.graph;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HPCurrent / HPMax;
        pos.x = Mathf.Clamp(pos.x, 0, globalData.getSize().x-1);
        pos.y = Mathf.Clamp(pos.y, 0, globalData.getSize().y-1);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
    void drawUnit()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = globalData.getUnitSprites();
        sp.sprite = sprites[(int)type];
        healthBar.fillAmount = 1;
    }
    void initialize()
    {
        type = unit.type;
        HPMax = unit.HPMax;
        HPCurrent = unit.HPCurrent;
        ATK = unit.ATK;
        cost = unit.cost;
        mobility = unit.mobility;
        range = unit.range;
        constructTime = unit.constructTime;

        status = Status.Unselected;

        if (enemy)
        {
            sp.flipX = true;
        }
    }
    private void OnMouseDown()
    {
        if (globalData.hasSelectedUnit())
        {
            globalData.resetSeclection();
        }
        else
        {
            globalData.setSelectedUnit(gameObject);
            switch (status)
            {
                case Status.Unselected: 
                    if (unit.type == UnitScriptable.Type.Interceptor) 
                        globalData.PathFindSkip();
                    else
                        globalData.pathFind();
                    break;
                case Status.Moved: globalData.PathFindSkip();
                    break;
            }
            
        }
    }
    public void moveTo(int x, int y)
    {
        pos.x = x;
        pos.y = y;
        status = Status.Moved;
    }
}
