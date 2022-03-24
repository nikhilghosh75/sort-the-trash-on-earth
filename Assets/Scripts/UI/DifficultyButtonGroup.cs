using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButtonGroup : MonoBehaviour
{
    public List<DifficultyButton> buttons;

    public ObjectSpawner spawner;
    public GameObject difficultyOverlay;

    [Header("Colors")]
    public Color selectedColor;
    public Color unselectedColor;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].difficultyButtonGroup = this;
            buttons[i].buttonIndex = i;
        }
        buttons[0].OnButtonClick();
        OnButtonSelected(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Options.gameMode == GameMode.Fast)
        {
            difficultyOverlay.SetActive(false);
        }
        else
        {
            difficultyOverlay.SetActive(true);
        }
    }

    public void SetDifficulty(float minTime, float maxTime, float speed)
    {
        spawner.minSpawnTime = minTime;
        spawner.maxSpawnTime = maxTime;
        spawner.fastSpeed = speed;
    }

    public void OnButtonSelected(int buttonIndex)
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if(i == buttonIndex)
            {
                buttons[i].SetColor(selectedColor);
            }
            else
            {
                buttons[i].SetColor(unselectedColor);
            }
        }
    }
}
