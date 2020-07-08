using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    // Start is called before the first frame update
    public enum State
    {
        unselected,
        selected,
        ranged
    }
    public GlobalData globalData;
    public Vector2Int pos;
    public GameObject unitOnTile;
    public State state;

    private SpriteRenderer sp;
    private Collider2D co;
    void Start()
    {
        globalData = GameObject.Find("GameManager").GetComponent<GlobalData>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        co = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /*if (co.OverlapPoint(mousePosition))
        {
            setSelected();
        }
        else
            unselected();*/
        /*if (co.OverlapPoint(mousePosition))
        {
            *//*if ((Input.GetMouseButton(1) || Input.GetMouseButton(0)) && !selected)
            {
                globalData.resetSeclection();
            }*//*
            if (Input.GetMouseButtonUp(0) && selected)
            {
                globalData.moveUnitTo(pos.x, pos.y);
            }
        }*/
    }
    private void OnMouseDown()
    {
        switch (state)
        {
            case State.selected: globalData.moveUnitTo(pos.x, pos.y);
                break;
            case State.ranged: 
                break;
        }
    }
    public void setData(int x, int y, GameObject unitOnTile)
    {
        pos.x = x;
        pos.y = y;
        this.unitOnTile = unitOnTile;
        this.state = State.unselected;
    }
    public void setSelected()
    {
        state = State.selected;
        toggleSelected();

    }
    public void unselected()
    {
        state = State.unselected;
        toggleSelected();
    }
    public void setRanged()
    {
        state = State.ranged;
        toggleSelected();
    }
    public void toggleSelected()
    {
        switch (state)
        {
            case State.unselected: sp.color = new Color(1f, 1f, 1f, 1f);
                break;
            case State.selected: sp.color = new Color(0.9f, 0.9f, 0.9f, 1f);
                break;
            case State.ranged: sp.color = new Color(1f, 0.4f, 0.4f, 1f);
                break;
        }
    }
}
