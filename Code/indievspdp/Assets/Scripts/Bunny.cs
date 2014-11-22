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
    Vector2 dragStart;
    Vector2 origPos;

    bool mouseOver, selected;
    bool moving = false;

    public bool Moving
    {
        get
        {
            return moving;
        }
        private set
        {
            moving = value;
        }
    }

    void Awake()
    {
        bunnyAnim = gameObject.GetComponentInChildren<BunnyAnimation>();
    }

    void Start()
    {
        origPos = gameObject.transform.position;
    }

    void Update()
    {
        if (mouseOver)
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
                //gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
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

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseDown()
    {
        if (mouseOver)
        {
            selected = true;
            dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseUp()
    {
        if (selected && mouseOver)
        {
            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(dragEnd);

            if (col)
            {
                if (col != gameObject.collider2D && col.gameObject.name == "Bunny")
                {
                    float sign = Mathf.Sign(dragStart.x - dragEnd.x);
                    RaycastHit2D[] colliders = Physics2D.RaycastAll(dragStart, new Vector2(dragStart.x - dragEnd.x, dragStart.y), (dragEnd.x - dragStart.x) * sign);

                    bool asd = false;

                    for (int i = 0; i < colliders.Length; ++i)
                    {
                        if (colliders[i].transform.GetComponent<Bunny>().Moving)
                        {
                            asd = true;
                        }
                    }

                    if (!asd)
                    {

                        dragStart = gameObject.transform.position;
                        dragEnd = col.gameObject.transform.position;

                        StartCoroutine(MoveToPosition(dragEnd));

                        for (int i = 0; i < colliders.Length; ++i)
                        {
                            Debug.Log(colliders.Length);
                            if (colliders[i].transform.name == "Bunny" && colliders[i].transform != gameObject.transform)
                            {
                                Debug.Log("asd");
                                StartCoroutine(colliders[i].transform.gameObject.GetComponent<Bunny>().MoveForward(sign));
                            }
                        }
                    }
                }
            }
        }
        selected = false;
        mouseOver = false;
    }

    void OnMouseExit()
    {
        if (!selected)
        {
            mouseOver = false;
        }
    }

    public IEnumerator MoveForward(float sign)
    {
        Moving = true;

        for (float f = 0f; f < 1.0f; f += Time.deltaTime)
        {

            gameObject.transform.position = origPos + new Vector2(1, 0) * f * sign;
            yield return null;
        }

        //gameObject.transform.position = origPos + new Vector2(1, 0) * sign;
        origPos = gameObject.transform.position;
        Moving = false;
    }

    public IEnumerator MoveToPosition(Vector2 pos)
    {
        Moving = true;

        for (float f = 0f; f < 1.0f; f += Time.deltaTime)
        {

            gameObject.transform.position = origPos + (pos - origPos) * f;
            yield return null;
        }

        gameObject.transform.position = pos;
        origPos = gameObject.transform.position;

        Moving = false;
    }
}
