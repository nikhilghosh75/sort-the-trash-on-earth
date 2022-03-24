using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem current;

    public Tooltip tooltip;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        Hide();
    }

    public void Show()
    {
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}
