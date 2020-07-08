using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalData globalData;
    public MapEditor mapEditor;
    private float panSpeed = 30f;
    private float panBorderThickness = Mathf.NegativeInfinity;
    private float scrollSpeed = 10f;
    private Vector2Int size;
    void Start()
    {
        resetCamera();
        //Vector3 pos = transform.position;
        //pos.x = (size.x - 1) / 2;
        //pos.y = (size.y - 1) / 2;
        //transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w")||Input.mousePosition.y >= Screen.height -panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("r"))
        {
            pos.x = (size.x-1) / 2 ;
            pos.y = (size.y-1) / 2 ;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize += -scroll * scrollSpeed * 200f * Time.deltaTime;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1f, 25f);

        pos.x = Mathf.Clamp(pos.x, 0, size.x);
        pos.y = Mathf.Clamp(pos.y, 0, size.y);

        transform.position = pos;
    }
    public void resetCamera()
    {
        if (globalData)
            size = globalData.getSize();
        else
            size = mapEditor.getSize();
    }
}
