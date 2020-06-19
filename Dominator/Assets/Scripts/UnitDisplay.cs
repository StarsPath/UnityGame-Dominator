using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    public UnitScriptable unit;
    public Image healthBar;
    SpriteRenderer sp;

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

    public bool attacked;
    public bool moved;
    public bool ended;

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
        transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
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

        attacked = false;
        moved = false;
        ended = false;
        if (enemy)
        {
            sp.flipX = true;
        }
    }
}
