using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject game;
    public GameObject mainMenu;

    public void StartGame()
    {
        mainMenu.SetActive(false);
        game.SetActive(true);

        game.GetComponent<GameController>().StartGame();
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        game.SetActive(false);
    }
}
