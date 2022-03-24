using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeOnSprite : MonoBehaviour
{
    public Vector2 maxSize;

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ResizeBasedOnSprite();
    }

    public void ResizeBasedOnSprite()
    {
        if(rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        Sprite sprite = GetComponent<Image>().sprite;
        Vector2 spriteSize = new Vector2(sprite.rect.width, sprite.rect.height);

        Vector2 scaledToWidth = spriteSize * (maxSize.x / spriteSize.x);
        Vector2 scaledToHeight = spriteSize * (maxSize.y / spriteSize.y);

        if (scaledToWidth.y <= maxSize.y)
        {
            rectTransform.SetWidth(maxSize.x);
            rectTransform.SetHeight(scaledToWidth.y);
        }
        else if (scaledToHeight.x <= maxSize.x)
        {
            rectTransform.SetWidth(scaledToHeight.x);
            rectTransform.SetHeight(maxSize.y);
        }
    }
}
