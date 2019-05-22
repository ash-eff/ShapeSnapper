using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public Shape[] shapes;
    public LayerMask clickMask;
    public float spawnSpeed;
    public int numberSpawned;

    Camera cam;
    float screenWidth;
    float screenHeight;
    float offset = .5f;

    private void Start()
    {
        cam = Camera.main;
        screenHeight = cam.orthographicSize - offset;
        screenWidth = cam.aspect * cam.orthographicSize - offset;

        StartCoroutine(SpawnShapes());
    }

    IEnumerator SpawnShapes()
    {
        while(true)
        {           
            float randX = Random.Range(-screenWidth, screenWidth);
            float randY = Random.Range(-screenHeight, screenHeight);
            Vector2 instPos = new Vector2(randX, randY);
            // shoot ray and check area
            RaycastHit2D hit = Physics2D.BoxCast(instPos, Vector2.one, 0, Vector2.zero);

            if (!hit) // if area is occupied, try again
            {
                numberSpawned++;
                int ind = Random.Range(0, shapes.Length);
                Instantiate(shapes[ind], instPos, Quaternion.identity);
                yield return new WaitForSecondsRealtime(spawnSpeed);
            }

            yield return null;
        }
    }
}
