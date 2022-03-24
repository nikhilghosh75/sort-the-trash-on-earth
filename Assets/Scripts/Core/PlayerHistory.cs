using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectHistory
{
    public string name;
    public Sprite sprite;
    public CanType type;
    public string reasoning;
    public int numCollected;
}

public class PlayerHistory : MonoBehaviour
{
    public static List<ObjectHistory> objectHistories = new List<ObjectHistory>();
    public static int bonuses = 0;

    public static void OnObjectSorted(ItemData itemData)
    {
        int objectIndex = FindObject(itemData.name);
        if(objectIndex == -1)
        {
            ObjectHistory history;
            history.name = itemData.name;
            history.numCollected = 1;
            history.reasoning = itemData.reasoning;
            history.sprite = itemData.sprite;
            history.type = itemData.correctType;
            objectHistories.Add(history);
        }
        else
        {
            ObjectHistory history = objectHistories[objectIndex];
            history.numCollected += 1;
            objectHistories[objectIndex] = history;
        }

        // Debug.Log(objectHistories.Count);
    }

    public static int GetNumObjectsSorted()
    {
        int numSorted = 0;
        for(int i = 0; i < objectHistories.Count; i++)
        {
            numSorted += objectHistories[i].numCollected;
        }
        return numSorted;
    }

    public static int GetNumUniqueSorted()
    {
        return objectHistories.Count;
    }

    public static int GetScore()
    {
        int numSorted = GetNumObjectsSorted();
        int uniqueSorted = GetNumUniqueSorted();

        int sortedScore = numSorted * 100;
        int uniqueScore = uniqueSorted * 13;
        int bonusesScore = bonuses;
        int finalScore = sortedScore + uniqueScore + bonusesScore;

        return finalScore;
    }

    public static void Reset()
    {
        objectHistories.Clear();
        bonuses = 0;
    }

    static int FindObject(string name)
    {
        for(int i = 0; i < objectHistories.Count; i++)
        {
            if(objectHistories[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
}
