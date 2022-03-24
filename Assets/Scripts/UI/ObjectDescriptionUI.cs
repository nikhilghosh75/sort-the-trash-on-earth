using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDescriptionUI : MonoBehaviour
{
    public ResizeOnSprite objectImage;
    public Text nameText;
    public Text categoryText;
    public Text reasoningText;

    public void SetObjectData(ObjectHistory objectData)
    {
        Image image = objectImage.GetComponent<Image>();

        image.sprite = objectData.sprite;
        objectImage.ResizeBasedOnSprite();

        nameText.text = objectData.name;
        categoryText.text = CanTypeToString(objectData.type);
        categoryText.color = CanTypeToColor(objectData.type);
        reasoningText.text = objectData.reasoning;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static string CanTypeToString(CanType type)
    {
        switch(type)
        {
            case CanType.Compost: return "Compost";
            case CanType.Hazardous: return "Hazardous";
            case CanType.Recycling: return "Recycling";
            case CanType.Trash: return "Trash";
        }
        return "";
    }

    static Color CanTypeToColor(CanType type)
    {
        switch (type)
        {
            case CanType.Compost: return new Color(139 / 255f, 69 / 255f, 19 / 255f);
            case CanType.Hazardous: return new Color(216 / 255f, 255 / 255f, 52 / 255f);
            case CanType.Recycling: return new Color(32 / 255f, 59 / 255f, 255 / 255f);
            case CanType.Trash: return Color.black;
        }
        return Color.white;
    }
}
