using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shape : MonoBehaviour
{
    public GameObject target;
    public GameObject explosion;
    public GameObject bonusPrefab;
    public GameObject totalScore;
    public Collider2D col;
    public GameObject outline;

    public bool matching = false;
    public bool selected = false;
    public bool matched = false;

    public int scoreToSend;
    public int bonus;
    private float speedVar = 10f;
    private float speed;

    private LineRenderer lr;
    private Animator anim;
    private Rigidbody2D rb2d;
    private GameController gc;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gc = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if(target != null)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, target.transform.position);
            speedVar += .4f;
            Vector3 direction = Vector3.MoveTowards(transform.position, target.transform.position, speedVar * Time.deltaTime);
            rb2d.MovePosition(direction);

            if (transform.position == target.transform.position && !matched)
            {
                matched = true;
                StartCoroutine(CheckMatch());
            }
        }
        else
        {
            lr.SetPosition(0, transform.position);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            lr.SetPosition(1, mousePos);
        }

        if (gc.GameOver)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator CheckMatch()
    {
        if (transform.tag == target.transform.tag)
        {
            gc.Run++;
            gc.PlayChime();

            if (bonus > 0)
            {
                scoreToSend = gc.Run * (bonus * 2);
            }
            else
            {
                scoreToSend = gc.Run;
            }

            gc.Score += scoreToSend;
            GameObject scoreObj = Instantiate(totalScore, target.transform.position, Quaternion.identity);
            scoreObj.GetComponent<TextMeshPro>().text = "+" + scoreToSend + "!!";
            Destroy(scoreObj, 2f);
            GameObject explosionObj = Instantiate(explosion, target.transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(target.gameObject);
            GameObject explosionObj2 = Instantiate(explosion, transform.position, Quaternion.identity);
            gc.ShapesOnScreen -= 2;
            Destroy(explosionObj2, 1f);
            Destroy(gameObject);
        }
        else
        {
            gc.Run = 0;
            gc.PlayChime();
            gc.Mismatch();
            GameObject explosionObj = Instantiate(explosion, target.transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(target.gameObject);
            GameObject explosionObj2 = Instantiate(explosion, transform.position, Quaternion.identity);
            gc.ShapesOnScreen -= 2;
            Destroy(explosionObj2, 1f);
            Destroy(gameObject);
        }

        yield return null;
    }

    public void ActivateCollider()
    {
        col.enabled = true;
    }

    public void LineEnabled(bool b)
    {
        lr.enabled = b;
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && !selected)
        {
            outline.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        outline.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.gameObject.layer == 9)
        {
            if(collision.gameObject.tag == transform.gameObject.tag && !collision.GetComponent<Shape>().matching)
            {
                gc.Run++;
                gc.PlayChime();
                bonus++;
                GameObject bonusObj = Instantiate(bonusPrefab, collision.transform.position, Quaternion.identity);
                Destroy(bonusObj, 1f);
                GameObject explosionObj = Instantiate(explosion, collision.transform.position, Quaternion.identity);
                Destroy(explosionObj, 1f);
                gc.ShapesOnScreen--;
                Destroy(collision.gameObject);
            }
            else
            {
                gc.Run = 0;
                gc.PlayChime();
                gc.Mismatch();
                GameObject explosionObj = Instantiate(explosion, collision.transform.position, Quaternion.identity);
                Destroy(explosionObj, 1f);
                gc.ShapesOnScreen--;
                Destroy(collision.gameObject);
                GameObject explosionObj2 = Instantiate(explosion, target.transform.position, Quaternion.identity);
                Destroy(explosionObj2, 1f);
                gc.ShapesOnScreen--;
                Destroy(target.gameObject);
                GameObject explosionObj3 = Instantiate(explosion, target.transform.position, Quaternion.identity);
                Destroy(explosionObj3, 1f);
                gc.ShapesOnScreen--;
                Destroy(gameObject);
            }
        }
    
        //if(transform.gameObject.layer == 8 && collision.gameObject.layer == 8)
        //{
        //    GameObject explosionObj = Instantiate(explosion, collision.transform.position, Quaternion.identity);
        //    Destroy(explosionObj, 1f);
        //    Destroy(collision.gameObject);
        //    GameObject explosionObj2 = Instantiate(explosion, transform.position, Quaternion.identity);
        //    Destroy(explosionObj, 1f);
        //    Destroy(gameObject);
        //}
    }
}
