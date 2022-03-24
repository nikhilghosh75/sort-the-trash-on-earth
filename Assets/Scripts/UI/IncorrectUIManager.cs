using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncorrectUIManager : MonoBehaviour
{
    public Image incorrectImage;
    public Text nameText;
    public Text descriptionText;
    public Button continueButton;

    public void SetButtonText(string text)
    {
        Text buttonText = continueButton.GetComponentInChildren<Text>();
        buttonText.text = text;
    }
}
