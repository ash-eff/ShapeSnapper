using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    private void Update()
    {
        scoreText.text = score.ToString("0000");
    }
}
