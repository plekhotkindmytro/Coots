using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject normalPrefab;
    public GameObject oddPrefab;

    private const float tileSize = 1f;
    private const float halfTileSize = tileSize / 2f;

    private struct OddPoss { 
        public int x;
        public int y;
        public OddPoss(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    };

    private OddPoss oddPoss = new OddPoss(4, 3);

    private Camera cam;
    void Start()
    {
        cam = Camera.main;

        float maxX = cam.orthographicSize * cam.aspect;
        float maxY = cam.orthographicSize;

        for (float i = -maxX; i < maxX; i++)
        {
            for(float j = -maxY; j < maxY; j++)
            {
                GameObject tile;
                if (i== oddPoss.x && j == oddPoss.y)
                {
                    tile = Instantiate(oddPrefab);
                } else
                {
                    tile = Instantiate(normalPrefab);
                }

                tile.transform.localPosition = new Vector2(i + halfTileSize, j + halfTileSize);
                tile.transform.parent = transform;
            }
        }
    }


}
