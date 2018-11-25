using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public bool isPlayer1 = true;
    void Update()
    {
        if (isPlayer1)
        {
            this.GetComponent<Text>().text = GameManager.instance.player1score.ToString();
        }
        else
        {
            this.GetComponent<Text>().text = GameManager.instance.player2score.ToString();
        }
    }
}
