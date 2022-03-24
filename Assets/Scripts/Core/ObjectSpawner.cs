using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    bool isSpawning = false;

    public GameObject bonus;
    public List<GameObject> objects;
    public float startY;
    public float startMinX;
    public float startMaxX;

    [Header("Fast Mode")]
    public float minSpawnTime;
    public float maxSpawnTime;
    public float fastSpeed;

    [Header("Slow Mode")]
    public float slowMoveTime;
    public int numItemsToSpawn = 4;
    public List<RectTransform> spawnPositions;

    Coroutine spawningCoroutine;
    int[] currentObjectIndicies = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        DestroyChildren();
        ConfigureOptions();
        DoStartGame();
    }

    public void StopGame()
    {
        StopCoroutine(spawningCoroutine);
    }

    public void DestroyChildren()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void ConfigureOptions()
    {
        isSpawning = true;
    }

    void DoStartGame()
    {
        GameMode mode = Options.gameMode;
        switch (mode)
        {
            case GameMode.Fast:
                spawningCoroutine = StartCoroutine(SpawnObjectsFast());
                break;
            case GameMode.Slow:
                spawningCoroutine = StartCoroutine(SpawnObjectsSlow());
                break;
        }
    }

    IEnumerator SpawnObjectsFast()
    {
        yield return new WaitForSeconds(0.8f);
        for(int i = 0; i < 3; i++)
        {
            int indexToSpawn = IndexOfObjectToSpawn();
            GameObject spawnedObject = Instantiate(objects[indexToSpawn], this.transform);
            ConfigureObjectFast(spawnedObject);
        }
        while(true)
        {
            if(!isSpawning)
            {
                yield break;
            }
            if(GameController.controller.isPaused)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            if(Random.Range(0.0f, 1.0f) < 0.05)
            {
                GameObject bonusObject = Instantiate(bonus, this.transform);
                bonusObject.GetComponent<RectTransform>().anchoredPosition = GetRandomSpawnPosition();
                yield return new WaitForSeconds(minSpawnTime);
            }

            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);

            int indexToSpawn = IndexOfObjectToSpawn();
            GameObject spawnedObject = Instantiate(objects[indexToSpawn], this.transform);
            spawnedObject.transform.SetAsFirstSibling();
            ConfigureObjectFast(spawnedObject);

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator SpawnObjectsSlow()
    {
        while(true)
        {
            if(transform.childCount == 0)
            {
                ResetObjectIndicies();
                for(int i = 0; i < numItemsToSpawn; i++)
                {
                    int indexToSpawn = IndexOfObjectToSpawn();
                    while (WasItemSpawned(indexToSpawn, i))
                    {
                        indexToSpawn = IndexOfObjectToSpawn();
                    }
                    GameObject spawnedObject = Instantiate(objects[indexToSpawn], this.transform);
                    ConfigureObjectSlow(spawnedObject, i);
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    void ConfigureObjectSlow(GameObject spawnedObject, int index)
    {
        RectTransform rectTransform = spawnedObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = GetRandomSpawnPosition();

        ObjectMover mover = spawnedObject.GetComponent<ObjectMover>();
        mover.SetPosition(spawnPositions[index].anchoredPosition, slowMoveTime);
    }

    void ConfigureObjectFast(GameObject spawnedObject)
    {
        RectTransform rectTransform = spawnedObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = GetRandomSpawnPosition();

        ObjectMover mover = spawnedObject.GetComponent<ObjectMover>();
        mover.speed = fastSpeed;
        mover.isFalling = true;
    }

    void ResetObjectIndicies()
    {
        currentObjectIndicies[0] = -1;
        currentObjectIndicies[1] = -1;
        currentObjectIndicies[2] = -1;
        currentObjectIndicies[3] = -1;
    }

    bool WasItemSpawned(int index, int currentIndex)
    {
        if(index == -1)
        {
            return true;
        }
        for(int i = 0; i < 4; i++)
        {
            if (i == currentIndex)
            {
                return false;
            }
            if(currentObjectIndicies[i] == index)
            {
                return true;
            }
        }
        return false;
    }

    int IndexOfObjectToSpawn()
    {
        return (int)Random.Range(0, objects.Count);
    }

    Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(startMinX, startMaxX), startY);
    }

    /*
    Vector2 GetRandomPositionInCollider()
    {
        Vector2 returnValue =  (Vector2)spawnRegion.bounds.center + new Vector2(
            (Random.value - 0.5f) * spawnRegion.bounds.size.x,
            (Random.value - 0.5f) * spawnRegion.bounds.size.y);
        return returnValue;
    }
    */
}
