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
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setData(int x, int y, GameObject unitOnTile, bool selected)
    {
        pos.x = x;
        pos.y = y;
        this.unitOnTile = unitOnTile;
        this.selected = selected;
    }
    public void toggleSelected()
    {
        selected = !selected;
        if (selected)
        {
            sp.color = new Color(0.9f, 0.9f, 0.9f, 1f);
        }
        else
            sp.color = new Color(1f, 1f, 1f, 1f);
    }
    /*private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
            sp.color = new Color(0.9f, 0.9f, 0.9f, 1f);
        if(Input.GetMouseButtonDown(1))
            sp.color = new Color(1f, 1f, 1f, 1f);
        //Debug.Log("HI");
    }*/
}
