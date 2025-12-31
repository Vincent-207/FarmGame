using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProtoTilesSpawner : MonoBehaviour
{
    [SerializeField]
    int width, height;
    [SerializeField]
    float tileWidth, tileHeight;
    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    Color primaryColor, secondaryColor;
    [SerializeField]
    Vector2 gridPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 gridCenter = Vector3.zero;
        
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Vector2 spawnPos = new Vector2((x - width/2) * tileWidth, (y - height/2) * tileHeight) + new Vector2(tileWidth/2, tileHeight/2) + gridPos;
                GameObject Tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);
                SpriteRenderer spriteRenderer = Tile.GetComponent<SpriteRenderer>();
                spriteRenderer.color = primaryColor;
                if(x  % 2 == 0 && y % 2 == 0)
                {
                    spriteRenderer.color = secondaryColor;
                }
                if(x  % 2 != 0 && y % 2 != 0)
                {
                    spriteRenderer.color = secondaryColor;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
