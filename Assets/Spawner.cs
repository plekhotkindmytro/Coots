using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject generalPrefab;
    public GameObject uniquePrefab;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject generalObject = Instantiate(generalPrefab);
            generalObject.transform.parent = transform;
        }

        GameObject uniqueObject = Instantiate(uniquePrefab);
        uniqueObject.transform.parent = transform;

    }

    public void Update()
    {
        
    }

    private Vector3 GetNewRandomPosition(GameObject gameObject)
    {
        float maxX = Camera.main.orthographicSize * Camera.main.aspect - gameObject.transform.localScale.x / 2;
        float maxY = Camera.main.orthographicSize - gameObject.transform.localScale.y / 2;
        float randomX = UnityEngine.Random.Range(0f, maxX);
        float randomY = UnityEngine.Random.Range(0f, maxY);
        int signX = UnityEngine.Random.value > 0.5f ? 1 : -1;
        int signY = UnityEngine.Random.value > 0.5f ? 1 : -1;
        randomX *= signX;
        randomY *= signY;
        Vector3 newPos = new Vector3(randomX, randomY, gameObject.transform.position.z);
        return newPos;
    }
}
