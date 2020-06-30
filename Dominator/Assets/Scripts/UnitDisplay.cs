using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
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

    private UnitScriptable.Type type;
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

    void Start()
    {
        globalData = GameObject.Find("GameManager").GetComponent<GlobalData>();
        initialize();
        sp = gameObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = globalData.getUnitSprites();
        sp.sprite = sprites[(int)type];
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HPCurrent / HPMax;
        pos.x = Mathf.Clamp(pos.x, 0, globalData.getSize().x-1);
        pos.y = Mathf.Clamp(pos.y, 0, globalData.getSize().y-1);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
    void OnMouseOver()
    {
        Debug.Log("Hovering");
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
    public void pathFind(int playerY, int playerX, int mobility)
    {
        int[,] cost = globalData.getTerrainCost();
        if (mobility >= 0)
        {
            movementOverlay[playerY, playerX] = 1;
        }
        if (!(playerY + 1 > globalData.mapSizeY))
            if (mobility - cost[playerY + 1, playerX] >= 0)
            {
                pathFind(playerY + 1, playerX, mobility - cost[playerY + 1, playerX]);
            }
        if (!(playerY - 1 < 0))
            if (mobility - cost[playerY - 1, playerX] >= 0)
            {
                pathFind(playerY - 1, playerX, mobility - cost[playerY - 1, playerX]);
            }
        if (!(playerX + 1 > globalData.mapSizeX))
            if (mobility - cost[playerY, playerX + 1] >= 0)
            {
                pathFind(playerY, playerX + 1, mobility - cost[playerY, playerX + 1]);
            }
        if (!(playerX - 1 < 0))
            if (mobility - cost[playerY, playerX - 1] >= 0)
            {
                pathFind(playerY, playerX - 1, mobility - cost[playerY, playerX - 1]);
            }
    }
    public void showMovementOverlay()
    {
        pathFind(pos.y, pos.x, mobility);
    }
    public void showAttackOverlay()
    {

    }
}
