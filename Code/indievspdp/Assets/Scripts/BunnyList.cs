using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunnyList : MonoBehaviour
{
    public const float BUNNY_INTERVAL = 1.5f;
    const int MAX_BUNNIES = 6;
    const float START_SPAWN_DELAY = 0.2f;


    List<GameObject> list;
    public int childCount;
    public GameObject bunnyPrefab;
    Vector2 nextPosition;

    Queue<int> asds;
    void Awake()
    {
        asds = new Queue<int>();
		Global.bunnyList = GetComponent<BunnyList>();
        list = new List<GameObject>();
        nextPosition = new Vector2(gameObject.transform.position.x + BUNNY_INTERVAL, gameObject.transform.position.y);

        for (int i = 0; i < childCount; i++)
        {
            nextPosition.x -= BUNNY_INTERVAL;
            GameObject bunny = Instantiate(bunnyPrefab, nextPosition, Quaternion.identity) as GameObject;
            bunny.transform.parent = gameObject.transform;
            bunny.transform.name = "Bunny";
            list.Add(bunny);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddBunny();
            ReturnItemCount(Bunny.BunnyType.Fireman);
        }
    }

    public void RemoveBunny(GameObject bunny)
    {
        Vector2 bunnyDeathPosition = bunny.transform.position;
        Vector2 bunnyEndPos = bunny.GetComponent<Bunny>().newPos;

        for (int i = childCount - 1; i >= 0; --i)
        {
            if (list[i] == bunny)
            {
                list.RemoveAt(i);
                Destroy(bunny, 1.0f);
            }
            else
            {
                if (bunnyEndPos != Vector2.zero)
                {
                    if (list[i].GetComponent<Bunny>().newPos.x < bunnyEndPos.x)
                    {
                        StartCoroutine(list[i].GetComponent<Bunny>().MoveForward(1));
                    }

                    else if (list[i].GetComponent<Bunny>().newPos == Vector2.zero && list[i].transform.position.x < bunnyDeathPosition.x)
                    {
                        StartCoroutine(list[i].GetComponent<Bunny>().MoveForward(1));
                    }
                }

                else if (list[i].transform.position.x < bunnyDeathPosition.x)
                {
                    if (list[i].GetComponent<Bunny>().newPos == Vector2.zero || list[i].GetComponent<Bunny>().newPos.x < bunnyDeathPosition.x)
                    {
                        StartCoroutine(list[i].GetComponent<Bunny>().MoveForward(1));
                    }
                }
            }
        }

        childCount--;
        nextPosition.x += BUNNY_INTERVAL;

		if (childCount < 1)
			Global.StopGame();
    }

    public void AddBunny()
    {
        if (childCount < MAX_BUNNIES)
        {
            nextPosition.x -= BUNNY_INTERVAL;
            GameObject bunny = Instantiate(bunnyPrefab, nextPosition, Quaternion.identity) as GameObject;
            bunny.transform.parent = gameObject.transform;
            bunny.transform.name = "Bunny";
            list.Add(bunny);
            childCount++;
        }
        else
        {
            Global.UICont.ExtraBunnyUp();
        }
    }

    public int ReturnItemCount(Bunny.BunnyType type)
    {
        int count = 0;

        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].GetComponent<Bunny>().ReturnType() == type)
            {
                count++;
            }
        }

        return count;
    }

    public void StartingBunnies(int count)
    {
        for (int k = 0; k < count; ++k)
        {
            StartCoroutine(StartingBunny());
        }
    }

    public IEnumerator StartingBunny()
    {
        int queueNumber = asds.Count;
        asds.Enqueue(queueNumber);

        while (asds.Peek() != queueNumber)
        {
            yield return null;
        }

        AddBunny();

        float f = 0f;
        while (f < START_SPAWN_DELAY)
        {
            f += Time.deltaTime;
            yield return null;
        }

        asds.Dequeue();
    }
}
