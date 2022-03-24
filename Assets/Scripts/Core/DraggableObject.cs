using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DraggableObject : MonoBehaviour
{
    Collider2D objectCollider;
    Vector2 mouseOffset;
    bool isDragged = false;

    public UnityEvent OnDragStart;
    public UnityEvent OnDragEnd;

    static DraggableObject currentDraggedObject;

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 mousePosition = Input.mousePosition;
        if (objectCollider.OverlapPoint(mousePosition))
        {
            if(Input.GetMouseButtonDown(0) && currentDraggedObject == null)
            {
                isDragged = true;
                mouseOffset = (mousePosition - rectTransform.anchoredPosition);
                OnDragStart.Invoke();
                currentDraggedObject = this;
            }
            else if(Input.GetMouseButtonUp(0) && currentDraggedObject == this)
            {
                isDragged = false;
                OnDragEnd.Invoke();
                currentDraggedObject = null;
            }
            else if(Input.GetMouseButton(0))
            {
                // rectTransform.anchoredPosition = mousePosition - mouseOffset;
            }
        }
        
        if(isDragged)
        {
            rectTransform.anchoredPosition = mousePosition - mouseOffset;
        }
    }

    public bool IsDragged() { return isDragged; }
}
