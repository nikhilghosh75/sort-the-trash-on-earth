using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string headerText;
    public string contentText;

    bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.current.tooltip.header.text = headerText;
        TooltipSystem.current.tooltip.content.text = contentText;
        TooltipSystem.current.Show();

        isOver = true;

        // Debug.Log(gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.current.Hide();
        Debug.Log(gameObject.name);

        isOver = false;
    }

    void OnDisable()
    {
        if(isOver)
        {
            TooltipSystem.current.Hide();
        }
    }
}
