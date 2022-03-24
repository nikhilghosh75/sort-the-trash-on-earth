﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInputField : MonoBehaviour
{
    TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerData.name = inputField.text;
    }
}
