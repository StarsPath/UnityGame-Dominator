using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class UnitScriptable : ScriptableObject
{
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
    //private Vector2Int pos;
    //private bool enemy;

    [SerializeField]
    public Sprite sprite;

    public Type type;
    public int HPMax;
    public int HPCurrent;
    public int ATK;
    public int cost;
    public int mobility;
    public int range;
    public int constructTime;
}
