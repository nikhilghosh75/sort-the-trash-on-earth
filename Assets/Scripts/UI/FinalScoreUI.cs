using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreUI : MonoBehaviour
{
    public int scoreSpeed = 13;
    public int finalScoreSpeed = 23;

    [Header("Text Fields")]
    public Text objectsSorted;
    public Text bonusesClaimed;
    public Text uniqueItemsSorted;
    public Text finalScoreText;

    [HideInInspector]
    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartUI()
    {
        StartCoroutine(DoScoreUI());
    }

    IEnumerator DoScoreUI()
    {
        uniqueItemsSorted.text = "";
        objectsSorted.text = "";
        bonusesClaimed.text = "";
        finalScoreText.text = "";

        int numSorted = PlayerHistory.GetNumObjectsSorted();
        int uniqueSorted = PlayerHistory.GetNumUniqueSorted();

        int sortedScore = numSorted * 100;
        int uniqueScore = uniqueSorted * 13;
        int bonusesScore = PlayerHistory.bonuses;
        int finalScore = sortedScore + uniqueScore + bonusesScore;

        // Sorted Score
        int shownScore = 0;
        int shownObjects = 0;
        while(shownScore < sortedScore)
        {
            if(shownObjects < numSorted)
            {
                shownObjects++;
            }
            shownScore += scoreSpeed;
            objectsSorted.text = "100 x " + shownObjects.ToString() + " = " + FormattingFunctions.NumberWithCommas(shownScore);
            yield return null;
        }
        objectsSorted.text = "100 x " + numSorted + " = " + FormattingFunctions.NumberWithCommas(sortedScore);

        // Unique Score
        shownScore = 0;
        shownObjects = 0;
        while (shownScore < uniqueScore)
        {
            if (shownObjects < uniqueSorted)
            {
                shownObjects++;
            }
            shownScore += scoreSpeed;
            uniqueItemsSorted.text = "13 x " + shownObjects.ToString() + " = " + shownScore.ToString();
            yield return null;
        }
        uniqueItemsSorted.text = "13 x " + uniqueSorted.ToString() + " = " + uniqueScore.ToString();

        // Bonuses
        shownScore = 0;
        while (shownScore < bonusesScore)
        {
            shownScore += scoreSpeed;
            bonusesClaimed.text = FormattingFunctions.NumberWithCommas(shownScore);
            yield return null;
        }
        bonusesClaimed.text = FormattingFunctions.NumberWithCommas(bonusesScore);

        // Final Score
        shownScore = 0;
        while (shownScore < finalScore)
        {
            shownScore += scoreSpeed;
            finalScoreText.text = FormattingFunctions.NumberWithCommas(shownScore);
            yield return null;
        }
        finalScoreText.text = FormattingFunctions.NumberWithCommas(finalScore);

        isDone = true;
    }
}
