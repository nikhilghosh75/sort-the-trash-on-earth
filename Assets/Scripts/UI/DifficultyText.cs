using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyText : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Options.gameMode == GameMode.Slow)
        {
            text.text = "Relaxed";
        }
        else
        {
            text.text = "Normal - " + Options.difficultyModeName;
        }
    }
}
