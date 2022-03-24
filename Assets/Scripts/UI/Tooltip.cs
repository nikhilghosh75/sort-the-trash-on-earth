using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public Text header;
    public Text content;

    public int characterWrapLimit;

    LayoutElement layout;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        layout = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(layout == null)
        {
            layout = GetComponent<LayoutElement>(); 
            rectTransform = GetComponent<RectTransform>();
        }

        int headerLength = header.text.Length;
        int contentLength = content.text.Length;

        layout.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;

        Vector2 pivot = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        rectTransform.pivot = pivot;
    }
}
