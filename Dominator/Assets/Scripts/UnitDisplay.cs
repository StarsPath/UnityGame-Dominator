using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public UnitScriptable unit;
    public Image healthBar;
    SpriteRenderer sp;

    public UnitScriptable.Type type;
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
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.sprite = unit.sprite;
        initialize();
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HPCurrent / HPMax;
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
