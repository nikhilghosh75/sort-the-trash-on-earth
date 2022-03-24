using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }

public class NToggle : MonoBehaviour
{
    public List<Button> toggles;
    public Sprite unSelectedImage;
    public Sprite selectedImage;
    public int defaultValue = 0;
    int value = 0;

    public IntEvent OnValueChanged;

    // Start is called before the first frame update
    void Start()
    {
        OnButtonPressed(defaultValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed(int button)
    {
        value = button;
        OnValueChanged.Invoke(button);
        for(int i = 0; i < toggles.Count; i++)
        {
            Image image = toggles[i].GetComponent<Image>();
            if(i == value)
            {
                image.sprite = selectedImage;
            }
            else
            {
                image.sprite = unSelectedImage;
            }
        }
    }

    public int GetValue()
    {
        return value;
    }
}
