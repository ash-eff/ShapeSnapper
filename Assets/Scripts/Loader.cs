using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject[] shapes;

    void Start()
    {
        StartCoroutine(ShapeSwap());
    }

    IEnumerator ShapeSwap()
    {
        while (true)
        {
            shapes[0].SetActive(true);
            yield return new WaitForSeconds(.5f);
            shapes[1].SetActive(true);
            yield return new WaitForSeconds(.5f);
            shapes[2].SetActive(true);
            yield return new WaitForSeconds(.5f);
            shapes[0].SetActive(false);
            shapes[1].SetActive(false);
            shapes[2].SetActive(false);
            yield return new WaitForSeconds(.5f);
            shapes[0].SetActive(true);
            shapes[1].SetActive(true);
            shapes[2].SetActive(true);
            yield return new WaitForSeconds(.5f);
            shapes[0].SetActive(false);
            shapes[1].SetActive(false);
            shapes[2].SetActive(false);
            yield return new WaitForSeconds(.5f);
            shapes[0].SetActive(true);
            shapes[1].SetActive(true);
            shapes[2].SetActive(true);
            yield return new WaitForSeconds(.5f);
            shapes[0].SetActive(false);
            shapes[1].SetActive(false);
            shapes[2].SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
