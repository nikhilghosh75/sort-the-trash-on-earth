using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [HideInInspector]
    public DifficultyButtonGroup difficultyButtonGroup;

    [HideInInspector]
    public int buttonIndex;

    [HideInInspector]
    public Image image;

    public Text text;

    public float minTime;
    public float maxTime;
    public float speed;
    public string difficultyName;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        difficultyButtonGroup.OnButtonSelected(buttonIndex);
        difficultyButtonGroup.SetDifficulty(minTime, maxTime, speed);

        Options.difficultyModeName = difficultyName;
    }

    public void SetColor(Color color)
    {
        if (image == null)
            image = GetComponent<Image>();

        image.color = color;
        text.color = color;
    }
}
