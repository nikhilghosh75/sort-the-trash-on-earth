using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject blockingImage;
    public GameObject title;
    public GameObject panel;

    public float freezeTime;
    public float titleTime;
    public float panelTime;
    public float titleStartPosition;
    public float titleEndPosition;
    public float panelStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoMainMenu());
    }

    IEnumerator DoMainMenu()
    {
        RectTransform titleTransform = title.GetComponent<RectTransform>();
        titleTransform.anchoredPosition = new Vector2(0, titleStartPosition);
        RectTransform panelTransform = panel.GetComponent<RectTransform>();
        panelTransform.anchoredPosition = new Vector2(0, panelStartPosition);

        blockingImage.SetActive(true);
        float currentTime = 0f;
        float speed;

        yield return new WaitForSeconds(freezeTime);
        speed = (titleEndPosition - titleStartPosition) / titleTime;

        while(currentTime < titleTime)
        {
            titleTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
            yield return null;
            currentTime += Time.deltaTime;
        }

        speed = panelStartPosition / titleTime;

        currentTime = 0;
        while (currentTime < panelTime / 2)
        {
            panelTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);
            yield return null;
            currentTime += Time.deltaTime;
        }

        panelTransform.anchoredPosition = new Vector2(0, 0);

        blockingImage.SetActive(false);
    }
}
