using UnityEngine;
using System.Collections;

public class Bunny : MonoBehaviour
{
    public enum BunnyType
    {
        None,
        Constructor,
        Fireman,
        Lumberjack
    }

    BunnyType bunnyType = BunnyType.None;
    BunnyAnimation bunnyAnim;

    void Awake()
    {
        bunnyAnim = gameObject.GetComponentInChildren<BunnyAnimation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeForm(BunnyType.None);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeForm(BunnyType.Fireman);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeForm(BunnyType.Constructor);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeForm(BunnyType.Lumberjack);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "HazardFire")
        {
            if (bunnyType == BunnyType.Fireman)
            {
                Debug.Log("Firemaaan");
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("Bunny burns");
                bunnyAnim.DeathAnimation();
                Destroy(col.gameObject);
            }
        }
        else if (col.transform.tag == "HazardHole")
        {
            if (bunnyType == BunnyType.Constructor)
            {
                Debug.Log("Constructooooor");
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("Bunny falls");
                bunnyAnim.DeathAnimation();
                Destroy(col.gameObject);
            }
        }
        else if (col.transform.tag == "HazardTree")
        {
            if (bunnyType == BunnyType.Lumberjack)
            {
                Debug.Log("Lumberjaaaaack");
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("Bunny bumps");
                bunnyAnim.DeathAnimation();
                Destroy(col.gameObject);
            }
        }
    }

    void ChangeForm(BunnyType type)
    {
        bunnyType = type;
        bunnyAnim.ChangeAnimation(bunnyType);
    }
}
