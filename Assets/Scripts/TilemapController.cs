using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    
    Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = this.gameObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Pelle")
        {
            Debug.Log("collide");
            
            foreach(ContactPoint2D contactPoint in collision.contacts)
            {
                tilemap.SetTile(tilemap.WorldToCell(contactPoint.point), null);
            }
            
        }
    }
}
