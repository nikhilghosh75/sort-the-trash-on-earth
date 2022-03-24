using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    public float easiestMinTime;
    public float easiestMaxTime;
    public float easiestSpeed;

    public float hardestMinTime;
    public float hardestMaxTime;
    public float hardestSpeed;

    public ObjectSpawner spawner;
    public GameObject difficultyOverlay;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChange);
    }

    void Update()
    {
        if(Options.gameMode == GameMode.Fast)
        {
            difficultyOverlay.SetActive(false);
        }
        else
        {
            difficultyOverlay.SetActive(true);
        }
    }

    void OnValueChange(float value)
    {
        spawner.minSpawnTime = Mathf.Lerp(easiestMinTime, hardestMinTime, value);
        spawner.maxSpawnTime = Mathf.Lerp(easiestMaxTime, hardestMaxTime, value);
        spawner.fastSpeed = Mathf.Lerp(easiestSpeed, hardestSpeed, value);
    }
}
