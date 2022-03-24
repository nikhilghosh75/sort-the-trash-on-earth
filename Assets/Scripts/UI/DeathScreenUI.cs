using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenUI : MonoBehaviour
{
    public GameObject objectDescriptionPrefab;

    public FinalScoreUI finalScoreUI;
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayUI()
    {
        StartCoroutine(DoDisplayUI());
    }

    public void ShowSortedObjects()
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();

        // Object Description Prefab
        for (int i = 0; i < PlayerHistory.objectHistories.Count; i++)
        {
            GameObject spawnedObject = Instantiate(objectDescriptionPrefab, content.transform);
            ObjectDescriptionUI descriptionUI = spawnedObject.GetComponent<ObjectDescriptionUI>();
            descriptionUI.SetObjectData(PlayerHistory.objectHistories[i]);
            contentRect.SetHeight(85 * i);
        }
    }

    IEnumerator DoDisplayUI()
    {
        // Reset
        for(int i = content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        // Final Score UI
        finalScoreUI.StartUI();
        while(!finalScoreUI.isDone)
        {
            yield return null;
        }

        ShowSortedObjects();
    }
}
