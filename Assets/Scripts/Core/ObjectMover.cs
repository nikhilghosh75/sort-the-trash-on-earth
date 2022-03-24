using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed;

    Vector2 direction;

    [HideInInspector]
    public bool isFalling = false;

    DraggableObject draggableObject;

    // Start is called before the first frame update
    void Start()
    {
        draggableObject = GetComponent<DraggableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (draggableObject == null)
        {
            if(!GameController.controller.isPaused)
            {
                rectTransform.anchoredPosition = rectTransform.anchoredPosition - new Vector2(0, speed * Time.deltaTime);
            }
            return;
        }

        GameMode mode = Options.gameMode;
        switch (mode)
        {
            case GameMode.Fast:
                if(!draggableObject.IsDragged() && !GameController.controller.isPaused)
                {
                    rectTransform.anchoredPosition = rectTransform.anchoredPosition - new Vector2(0, speed * Time.deltaTime);
                }
                break;
            case GameMode.Slow:
                if(speed > 0.01 && !draggableObject.IsDragged())
                {
                    Debug.Log(speed * direction * Time.deltaTime);
                    rectTransform.anchoredPosition = rectTransform.anchoredPosition - speed * direction * Time.deltaTime;
                }
                break;
        }

    }

    public void SetPosition(Vector2 newPosition, float time)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        direction = ((Vector2)rectTransform.anchoredPosition - newPosition).normalized;
        speed = Vector2.Distance(rectTransform.anchoredPosition, newPosition) / time;
        Invoke("Stop", time);

        // Debug.Log(direction);
    }

    void Stop()
    {
        speed = 0;
    }
}
