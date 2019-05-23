using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public Shape[] shapes;
    public LayerMask clickMask;
    public float maxShapes;

    private GameController gc;
    private Camera cam;
    private float screenWidth;
    private float screenHeight;
    private float offset = .5f;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
        screenHeight = cam.orthographicSize - offset;
        screenWidth = cam.aspect * cam.orthographicSize - offset;

        StartCoroutine(SpawnShapes());
    }

    IEnumerator SpawnShapes()
    {
        while(!gc.GameOver)
        {           
            while(gc.ShapesOnScreen < maxShapes)
            {
                float randX = Random.Range(-screenWidth, screenWidth);
                float randY = Random.Range(-screenHeight, screenHeight);
                Vector2 instPos = new Vector2(randX, randY);
                // shoot ray and check area
                RaycastHit2D hit = Physics2D.BoxCast(instPos, Vector2.one, 0, Vector2.zero);

                if (!hit) // if area is occupied, try again
                {
                    gc.ShapesOnScreen++;
                    int ind = Random.Range(0, shapes.Length);
                    Instantiate(shapes[ind], instPos, Quaternion.identity);
                    yield return new WaitForSeconds(gc.SpawnSpeed);
                }

                yield return null;
            }

            yield return null;
        }
    }
}
