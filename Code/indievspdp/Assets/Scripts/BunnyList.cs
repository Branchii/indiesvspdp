using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunnyList : MonoBehaviour
{
    public const float BUNNY_INTERVAL = 1.5f;

    List<GameObject> list;
    public int childCount;
    public GameObject bunnyPrefab;
    Vector2 nextPosition;
    void Awake()
    {
		Global.bunnyList = GetComponent<BunnyList>();

        list = new List<GameObject>();

        Vector2 startPosition = gameObject.transform.position;
        nextPosition = startPosition;
        GameObject firstBunny = Instantiate(bunnyPrefab, startPosition, Quaternion.identity) as GameObject;
        firstBunny.transform.parent = gameObject.transform;
        firstBunny.transform.name = "Bunny";
        list.Add(firstBunny);

        for (int i = 0; i < childCount - 1; i++)
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
    }

    public void AddBunny()
    {
        nextPosition.x -= BUNNY_INTERVAL;
        GameObject bunny = Instantiate(bunnyPrefab, nextPosition, Quaternion.identity) as GameObject;
        bunny.transform.parent = gameObject.transform;
        bunny.transform.name = "Bunny";
        list.Add(bunny);
        childCount++;
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
}
