using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDamage : MonoBehaviour
{

    GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit " + collision.transform.parent.name);
        //if (collision.gameObject.tag == "Damage")
        //{
            if (transform.parent.tag == collision.transform.parent.tag)
            {
                gc.score++;
                Destroy(gameObject.transform.parent);
            }
            else
            {
                gc.score--;
                Destroy(collision.transform.parent);
                Destroy(gameObject.transform.parent);
            }
        //}
    }
}
