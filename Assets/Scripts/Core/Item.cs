using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemData
{
    public string name;
    public CanType correctType;
    [TextArea]
    public string reasoning;

    [HideInInspector]
    public Sprite sprite;
}

public class Item : MonoBehaviour
{
    public ItemData itemData;

    DraggableObject draggable;
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        draggable = GetComponent<DraggableObject>();
        collider = GetComponent<Collider2D>();

        Image image = GetComponent<Image>();
        itemData.sprite = image.sprite;
    }

    private void Update()
    {
        if(!draggable.IsDragged())
        {
            CheckColliders();
        }
    }

    // Update is called once per frame
    public void CheckColliders()
    {
        Collider2D[] results = new Collider2D[2];
        int collidersColliding = collider.OverlapCollider(new ContactFilter2D(), results);
        if(collidersColliding > 0)
        {
            Can currentCan = results[0].GetComponent<Can>();
            if(currentCan == null)
            {
                if(collidersColliding < 2)
                {
                    return;
                }
                currentCan = results[1].GetComponent<Can>();
            }

            if(currentCan != null)
            {
                CanType currentType = currentCan.canType;
                if (currentType == itemData.correctType)
                {
                    GameController.controller.OnItemDropped(true, itemData);
                }
                else
                {
                    GameController.controller.OnItemDropped(false, itemData);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
