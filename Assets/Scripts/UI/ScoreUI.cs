using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text text;
    int displayAmount = 0;

    public bool shouldUpdate;
    public int scoreSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUpdate)
        {
            int currentScore = GameController.controller.score;
            if (displayAmount != currentScore)
            {
                if (currentScore > displayAmount)
                {
                    displayAmount = Mathf.Min(displayAmount + scoreSpeed, currentScore);
                }
                else
                {
                    displayAmount = Mathf.Max(displayAmount - scoreSpeed, currentScore);
                }
                text.text = FormattingFunctions.NumberWithCommas(displayAmount);
            }
            else
            {
                text.text = FormattingFunctions.NumberWithCommas(currentScore);
            }
        }
        else
        {
            int currentScore = GameController.controller.score;
            text.text = currentScore.ToString();
        }
    }

    public void Reset()
    {
        displayAmount = GameController.controller.score;
    }
}
