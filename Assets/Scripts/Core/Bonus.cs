using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bonus : MonoBehaviour, IPointerDownHandler
{
    public int bonusAmount = 200;

    public void OnClick()
    {
        PlayerHistory.bonuses += bonusAmount;
        GameController.controller.IncreaseScore(bonusAmount);
        Destroy(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }
}
