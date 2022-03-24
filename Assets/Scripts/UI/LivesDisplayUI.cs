using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplayUI : MonoBehaviour
{
    public GameObject livesPrefab;
    public float livesWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int numLivesDisplayed = transform.childCount;
        
        if(numLivesDisplayed != GameController.controller.lives)
        {
            if(numLivesDisplayed > GameController.controller.lives)
            {
                Destroy(transform.GetChild(numLivesDisplayed - 1).gameObject);
            }
            else
            {
                GameObject tempObject = Instantiate(livesPrefab, this.transform);
                RectTransform rectTransform = tempObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector3(livesWidth * (numLivesDisplayed + 0.5f), 0, 0);
            }
        }
    }
}
