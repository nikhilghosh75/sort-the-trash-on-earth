using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public GameObject correctObject;
    public GameObject incorrectObject;
    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        correctObject.SetActive(false);
        incorrectObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        deathScreen.SetActive(false);
        correctObject.SetActive(false);
        incorrectObject.SetActive(false);
    }

    public void OnCorrectDropped(ItemData data)
    {
        Can can = Can.Find(data.correctType);
        RectTransform canTransform = can.GetComponent<RectTransform>();
        RectTransform rectTransform = correctObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = canTransform.anchoredPosition + can.correctTextOffset;

        ShowCorrect();
        CancelInvoke("HideCorrect");
        Invoke("HideCorrect", 2f);
    }

    public void OnIncorrectDropped(ItemData data)
    {
        int lives = GameController.controller.lives;
        IncorrectUIManager incorrectManager = incorrectObject.GetComponent<IncorrectUIManager>();

        Image incorrectImage = incorrectManager.incorrectImage;
        incorrectImage.sprite = data.sprite;
        incorrectImage.GetComponent<ResizeOnSprite>().ResizeBasedOnSprite();

        incorrectManager.nameText.text = data.name;
        incorrectManager.descriptionText.text = data.reasoning;

        incorrectManager.SetButtonText(lives <= 0 ? "End Game" : "Continue");

        ShowIncorrect();
    }

    public void ShowCorrect()
    {
        correctObject.SetActive(true);
    }

    public void HideCorrect()
    {
        correctObject.SetActive(false);
    }

    public void ShowIncorrect()
    {
        incorrectObject.SetActive(true);
        GameController.controller.PauseGame();
    }

    public void HideIncorrect()
    {
        incorrectObject.SetActive(false);
        if(GameController.controller.lives <= 0)
        {
            GameController.controller.EndGame();
        }
        else
        {
            GameController.controller.UnpauseGame();
        }
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        GameController.controller.PauseGame();
    }
}
