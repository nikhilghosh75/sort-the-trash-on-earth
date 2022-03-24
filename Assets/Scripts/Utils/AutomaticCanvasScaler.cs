using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticCanvasScaler : MonoBehaviour
{
    public static AutomaticCanvasScaler canvasScaler;

    public int originalWidth;
    public int originalHeight;

    CanvasScaler scaler;

    // Start is called before the first frame update
    void Start()
    {
        canvasScaler = this;
        scaler = GetComponent<CanvasScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        scaler.scaleFactor = ((float)Screen.width / originalWidth);
    }

    public static float GetCanvasScale()
    {
        return canvasScaler.GetComponent<CanvasScaler>().scaleFactor;
    }
}
