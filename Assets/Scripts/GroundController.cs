using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundController : MonoBehaviour
{

    public GameObject tileMapGameObject;

    public Camera camera;

    Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        if(tileMapGameObject != null)
        {
            tilemap = tileMapGameObject.GetComponent<Tilemap>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition.x + " - " + mousePosition.y);
        tilemap.SetTile(tilemap.WorldToCell(mousePosition), null);
        tilemap.SetTile(tilemap.WorldToCell(mousePosition + (Vector3.right * 0.125f)), null);
        tilemap.SetTile(tilemap.WorldToCell(mousePosition + Vector3.up * 0.125f), null);
        tilemap.SetTile(tilemap.WorldToCell(mousePosition + Vector3.down * 0.125f), null);
        tilemap.SetTile(tilemap.WorldToCell(mousePosition + Vector3.left * 0.125f), null);

    }


}
