using UnityEngine;
using System.Collections;

public class Bunny : MonoBehaviour
{
    enum BunnyType
    {
        None,
        Constructor,
        Fireman,
        Lumberjack
    }

    BunnyType bunnyType;
    Animator anim;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        bunnyType = BunnyType.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeType(BunnyType.Fireman);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "Hazard Fire")
        {
            if (bunnyType == BunnyType.Fireman)
            {
                Debug.Log("Bunnyfiremaaan");
                col.gameObject.GetComponent<Fire>().Extinguish();
            }
            else
            {
                Debug.Log("Bunny burns");
                Destroy(gameObject);
                col.gameObject.GetComponent<Fire>().Extinguish();
            }
        }
    }

    void ChangeType(BunnyType type)
    {
        bunnyType = type;

        switch (bunnyType)
        {
            case BunnyType.Fireman:
                anim.SetBool("fireman", true);
                break;
        }
    }
}
