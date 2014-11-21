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
            ChangeForm(BunnyType.Fireman);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "HazardFire")
        {
            if (bunnyType == BunnyType.Fireman)
            {
                Debug.Log("Bunnyfiremaaan");
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("Bunny burns");
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
