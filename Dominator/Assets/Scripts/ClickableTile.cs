using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2Int pos;
    public GameObject unitOnTile;
    public bool selected;

    private SpriteRenderer sp;
    private Collider2D co;
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        co = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /*if (co.OverlapPoint(mousePosition))
        {
            setSelected();
        }
        else
            unselected();*/
    }
    public void setData(int x, int y, GameObject unitOnTile, bool selected)
    {
        pos.x = x;
        pos.y = y;
        this.unitOnTile = unitOnTile;
        this.selected = selected;
    }
    public void setSelected()
    {
        selected = true;
        toggleSelected();

    }
    public void unselected()
    {
        selected = false;
        toggleSelected();
    }
    public void toggleSelected()
    {
        if (selected)
        {
            sp.color = new Color(0.9f, 0.9f, 0.9f, 1f);
        }
        else
            sp.color = new Color(1f, 1f, 1f, 1f);
    }
}
