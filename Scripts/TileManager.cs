using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TilemapCollider2D walls;
    public GameObject HG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log(collision.transform.gameObject.name);
        tilemap.SetColor(tilemap.WorldToCell(collision.transform.position), Color.red);
        
        
       
    }
}
