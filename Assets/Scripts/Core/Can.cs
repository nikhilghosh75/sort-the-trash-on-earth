using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanType
{
    Trash,
    Recycling,
    Compost,
    Hazardous
}

public class Can : MonoBehaviour
{
    public static List<Can> cans = new List<Can>();

    public CanType canType;
    public Vector2 correctTextOffset;

    // Start is called before the first frame update
    void Start()
    {
        cans.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Can Find(CanType type)
    {
        for(int i = 0; i < cans.Count; i++)
        {
            if(cans[i].canType == type)
            {
                return cans[i];
            }
        }
        return null;
    }
}
