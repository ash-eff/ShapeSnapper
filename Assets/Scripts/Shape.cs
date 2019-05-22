using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject target;
    public GameObject explosion;
    public Collider2D col;
    public bool matching = false;
    public bool selected = false;
    public bool matched = false;
    private float speedVar = 10f;


    private Animator anim;
    private Rigidbody2D rb2d;
    private float speed;
    GameController gc;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gc = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if(target != null)
        {
            speedVar += .4f;
            Vector3 direction = Vector3.MoveTowards(transform.position, target.transform.position, speedVar * Time.deltaTime);
            rb2d.MovePosition(direction);

            if (transform.position == target.transform.position && !matched)
            {
                matched = true;
                StartCoroutine(CheckMatch());
            }
        }
    }

    public void ActivateCollider()
    {
        col.enabled = true;
    }

    IEnumerator CheckMatch()
    {
        if(transform.tag == target.transform.tag)
        {
            //yield return new WaitForSecondsRealtime(.3f);
            gc.score += 2;
            GameObject explosionObj = Instantiate(explosion, target.transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(target.gameObject);
            GameObject explosionObj2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionObj2, 1f);
            Destroy(gameObject);
        }
        else
        {
            gc.score -= 2;
            GameObject explosionObj = Instantiate(explosion, target.transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(target.gameObject);
            GameObject explosionObj2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionObj2, 1f);
            Destroy(gameObject);
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.gameObject.layer == 9)
        {
            if(collision.gameObject.tag == transform.gameObject.tag && !collision.GetComponent<Shape>().matching)
            {
                gc.score += 3;
                // instantiate bonus object
                GameObject explosionObj = Instantiate(explosion, collision.transform.position, Quaternion.identity);
                Destroy(explosionObj, 1f);
                Destroy(collision.gameObject);
            }
            else
            {
                gc.score -= 3;
                GameObject explosionObj = Instantiate(explosion, collision.transform.position, Quaternion.identity);
                Destroy(explosionObj, 1f);
                Destroy(collision.gameObject);
                GameObject explosionObj2 = Instantiate(explosion, target.transform.position, Quaternion.identity);
                Destroy(explosionObj2, 1f);
                Destroy(target.gameObject);
                GameObject explosionObj3 = Instantiate(explosion, target.transform.position, Quaternion.identity);
                Destroy(explosionObj3, 1f);
                Destroy(gameObject);
            }
        }

        if(transform.gameObject.layer == 8 && collision.gameObject.layer == 8)
        {
            GameObject explosionObj = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(collision.gameObject);
            GameObject explosionObj2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(gameObject);
        }
    }
}
