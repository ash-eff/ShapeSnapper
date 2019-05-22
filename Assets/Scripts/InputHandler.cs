using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject selectedItem;
    public GameObject checkItem;
    public LayerMask clickMask;

    void Update()
    {
        // FIRST CLICK
        FirstClick();

        // MOUSE OVER ITEM
        HoverOver();

        // RELEASE MOUSE BUTTON
        Release();
    }

    void FirstClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickMask);

            if (hit)
            {
                selectedItem = hit.collider.gameObject;
                selectedItem.GetComponent<Shape>().selected = true;
            }
        }
    }

    void HoverOver()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickMask);

            if (hit2)
            {
                if (hit2.collider.gameObject != selectedItem && checkItem == null)
                {
                    checkItem = hit2.collider.gameObject;
                    checkItem.GetComponent<Shape>().selected = true;
                }
            }
            else
            {
                if(checkItem != null)
                {
                    checkItem.GetComponent<Shape>().selected = false;
                    checkItem = null;
                }
            }
        }
    }

    void Release()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MatchCheck();
        }
    }

    void MatchCheck()
    {
        // magentize items if two items are selected and then clear items
        if(selectedItem != null && checkItem != null)
        {
            MagentizeItems(selectedItem, checkItem);
            selectedItem.GetComponent<Shape>().selected = false;
            checkItem.GetComponent<Shape>().selected = false;
            selectedItem = null;
            checkItem = null;
        }
        // reset if only one item is selected
        else
        {
            if(selectedItem != null)
            {
                selectedItem.GetComponent<Shape>().selected = false;
                //checkItem.GetComponent<Shape>().selected = false;
                selectedItem = null;
                //checkItem = null;
            }
        }
    }

    void MagentizeItems(GameObject objOne, GameObject objTwo)
    {
        float dist = (objTwo.transform.position - objOne.transform.position).magnitude;
        objOne.layer = 9;
        objTwo.layer = 9;
        objOne.GetComponent<Shape>().target = objTwo;
        //objTwo.GetComponent<Shape>().target = objOne;
        objOne.GetComponent<Shape>().matching = true;
        objTwo.GetComponent<Shape>().matching = true;
        //objOne.GetComponent<Shape>().circleCol.radius = dist;
        //objTwo.GetComponent<Shape>().circleCol.radius = dist;
    }
}
