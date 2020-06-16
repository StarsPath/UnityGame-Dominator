using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    // Start is called before the first frame update
    public enum Type
    {
        Null,
        Scout,
        Light,
        Heavy,
        Sniper,
        Knight,
        Artillery,
        Interceptor
    }
    private Vector2Int pos;

    [SerializeField]
    private Type type;
    private int HPMax;
    private int HPCurrent;
    private int ATK;
    private int cost;
    private int mobility;
    private int range;
    private int constructTime;
    private bool enemy;

    private bool attacked;
    private bool moved;
    private bool ended;

    public Unit(Type type, Vector2Int pos, bool enemy)
    {
        this.pos = pos;
        this.type = type;
        this.enemy = enemy;
        switch (type){
            case Type.Null:
                this.HPMax = 0;
                this.HPCurrent = 0;
                this.ATK = 0;
                this.cost = 0;
                this.mobility = 0;
                this.range = 0;
                this.constructTime = 0;
                break;
            case Type.Scout:
                this.HPMax = 100;
                this.HPCurrent = 100;
                this.ATK = 10;
                this.cost = 100;
                this.mobility = 5;
                this.range = 5;
                this.constructTime = 2;
                break;
            case Type.Light:
                this.HPMax = 150;
                this.HPCurrent = 150;
                this.ATK = 25;
                this.cost = 150;
                this.mobility = 5;
                this.range = 5;
                this.constructTime = 2;
                break;
            case Type.Heavy:
                this.HPMax = 300;
                this.HPCurrent = 300;
                this.ATK = 40;
                this.cost = 300;
                this.mobility = 4;
                this.range = 4;
                this.constructTime = 3;
                break;
            case Type.Sniper:
                this.HPMax = 100;
                this.HPCurrent = 120;
                this.ATK = 70;
                this.cost = 300;
                this.mobility = 4;
                this.range = 8;
                this.constructTime = 3;
                break;
            case Type.Knight:
                this.HPMax = 200;
                this.HPCurrent = 400;
                this.ATK = 70;
                this.cost = 500;
                this.mobility = 6;
                this.range = 2;
                this.constructTime = 4;
                break;
            case Type.Artillery:
                this.HPMax = 200;
                this.HPCurrent = 200;
                this.ATK = 70;
                this.cost = 350;
                this.mobility = 3;
                this.range = 10;
                this.constructTime = 4;
                break;
            case Type.Interceptor:
                this.HPMax = 150;
                this.HPCurrent = 200;
                this.ATK = 30;
                this.cost = 350;
                this.mobility = 10;
                this.range = 6;
                this.constructTime = 4;
                break;
        }
    }

    public Unit(Type type, Vector2Int newPos, int HPMax, int HPCurrent, int ATK, int cost, int mobility, int range, int constructTime)
    {
        this.type = type;
        this.pos = newPos;
        this.HPMax = HPMax;
        this.HPCurrent = HPCurrent;
        this.ATK = ATK;
        this.cost = cost;
        this.mobility = mobility;
        this.range = range;
        this.constructTime = constructTime;
    }

    public Vector2Int getPos()
    {
        return this.pos;
    }
    public void setPos(int x, int y)
    {
        this.pos.x = x;
        this.pos.y = y;
    }
    public void setPos(Vector2Int pos)
    {
        this.pos = pos;
    }
    public bool isDead()
    {
        if (this.HPCurrent > 0)
            return false;
        else
            return true;
    }
    public void takeDMG(int DMG)
    {
        this.HPCurrent -= DMG;
    }
    public void takePermDMG(int DMG)
    {
        this.HPCurrent -= DMG;
        this.HPMax -= (int)(DMG / 2);
    }
    public void repair(int heal)
    {
        if (this.HPCurrent < HPMax)
            this.HPCurrent = (int)Mathf.Clamp(this.HPCurrent += heal, 0, this.HPMax);
    }





    public Type getType()
    {
        return type;
    }
    public int getCost()
    {
        return this.cost;
    }
    public int getMobility()
    {
        return this.mobility;
    }
    public int getRange()
    {
        return this.range;
    }
    public int getConstructionTime()
    {
        return this.constructTime;
    }
}
