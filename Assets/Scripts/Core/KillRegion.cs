using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRegion : MonoBehaviour
{
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] results = new Collider2D[5];
        int collidersColliding = collider.OverlapCollider(new ContactFilter2D(), results);

        for(int i = 0; i < collidersColliding; i++)
        {
            Item item = results[i].GetComponent<Item>();
            if(item)
            {
                Debug.Log("Destroyed " + item.gameObject.name);
                Destroy(item.gameObject);
                GameController.controller.LoseLife();
            }
        }
    }
}
