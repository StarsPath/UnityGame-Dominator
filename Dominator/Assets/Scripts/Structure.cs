using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure
{
    // Start is called before the first frame update
    public enum Type
    {
        Tree,
        Boulder,
        Crystal,
        Bunker,
        Barrack,
        HQ,
        Factory,
        Lab,
        Repair
    }
    private Vector2Int pos;

    private bool resource;
    private Type type;
    private int HPMax;
    private int HPCurrent;
    private int ATK;
    private int width;
    private int height;
    private int cost;
    private int range;

    private Unit[] queue;

    private bool enemy;
    private bool attacked;

    public Structure(bool resource, Vector2Int pos, Type type, bool enemy)
    {
        this.resource = resource;
        this.pos = pos;
        this.type = type;
        this.enemy = enemy;
        if (!this.resource)
        {
            switch (this.type)
            {
                case Type.Bunker:
                    this.HPMax = 800;
                    this.HPCurrent = 800;
                    this.ATK = 100;
                    this.width = 1;
                    this.height = 1;
                    this.cost = 400;
                    this.range = 7;
                    break;
                case Type.Barrack:
                    this.HPMax = 1000;
                    this.HPCurrent = 1000;
                    this.ATK = 0;
                    this.width = 1;
                    this.height = 1;
                    this.cost = 300;
                    this.range = 0;
                    break;
                case Type.HQ:
                    this.HPMax = 2000;
                    this.HPCurrent = 2000;
                    this.ATK = 80;
                    this.width = 2;
                    this.height = 2;
                    this.cost = 2000;
                    this.range = 6;
                    break;
                case Type.Factory:
                    this.HPMax = 1000;
                    this.HPCurrent = 1000;
                    this.ATK = 100;
                    this.width = 2;
                    this.height = 2;
                    this.cost = 500;
                    this.range = 0;
                    break;
                case Type.Lab:
                    this.HPMax = 800;
                    this.HPCurrent = 800;
                    this.ATK = 100;
                    this.width = 2;
                    this.height = 1;
                    this.cost = 500;
                    this.range = 0;
                    break;
                case Type.Repair:
                    this.HPMax = 800;
                    this.HPCurrent = 800;
                    this.ATK = 100;
                    this.width = 1;
                    this.height = 1;
                    this.cost = 500;
                    this.range = 0;
                    break;
            }
        }
            
    }
    public bool isResource()
    {
        return this.resource;
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

}
