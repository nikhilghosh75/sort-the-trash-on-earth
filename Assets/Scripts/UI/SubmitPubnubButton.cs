using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitPubnubButton : MonoBehaviour
{
    Button button;
    public GameObject submittedGameObject;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        submittedGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        PubnubConnection.SubmitEmail(PlayerData.name, PlayerData.email, PlayerHistory.GetScore());
        submittedGameObject.SetActive(true);
    }
}
