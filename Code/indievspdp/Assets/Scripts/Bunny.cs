using UnityEngine;
using System.Collections;

public class Bunny : MonoBehaviour
{
    public enum BunnyType
    {
        None,
        Lumberjack,
        Fireman,
        Pinwheel
    }

    BunnyType bunnyType = BunnyType.None;
    BunnyAnimation bunnyAnim;
    Vector2 dragStart;
    Vector2 origPos;
    Ray2D ray = new Ray2D(Vector2.zero, Vector2.right);

    bool mouseOver, selected;
    bool moving = false;
    bool dying = false;

    public bool Dying
    {
        get { return dying; }
        private set
        {
            if (value)
            {
                selected = false;
            }
            dying = value;
        }
    }

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
                ChangeForm(BunnyType.Pinwheel);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                ChangeForm(BunnyType.Lumberjack);
            }
        }
    }

    void FixedUpdate()
    {
        if (!dying)
        {
            Debug.DrawRay(gameObject.transform.position, Vector2.right * 1.0f);
            int layerMask = 1 << 8;
            RaycastHit2D obstacle = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1.0f, layerMask);

            if (obstacle)
            {
                Debug.Log("hitt");

                if (obstacle.collider.transform.tag == "HazardFire")
                {
                    if (bunnyType == BunnyType.Fireman)
                    {
                        Debug.Log("Firemaaan");
                        Destroy(obstacle.collider.gameObject);
                    }
                    else
                    {
                        Debug.Log("Bunny burns");
                        bunnyAnim.DeathAnimation();
                        Dying = true;
                        Destroy(obstacle.collider.gameObject);
                    }
                }
                else if (obstacle.collider.transform.tag == "HazardRain")
                {
                    if (bunnyType == BunnyType.Pinwheel)
                    {
                        Debug.Log("Pinwheeeeeeel");
                        Destroy(obstacle.collider.gameObject);
                    }
                    else
                    {
                        Debug.Log("Bunny drowns");
                        bunnyAnim.DeathAnimation();
                        Dying = true;
                        Destroy(obstacle.collider.gameObject);
                    }
                }
                else if (obstacle.collider.transform.tag == "HazardTree")
                {
                    if (bunnyType == BunnyType.Lumberjack)
                    {
                        Debug.Log("Lumberjaaaaack");
                        Destroy(obstacle.collider.gameObject);
                    }
                    else
                    {
                        Debug.Log("Bunny bumps");
                        bunnyAnim.DeathAnimation();
                        Dying = true;
                        Destroy(obstacle.collider.gameObject);
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BubbleWater")
        {
            ChangeForm(BunnyType.Fireman);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "BubbleSaw")
        {
            ChangeForm(BunnyType.Lumberjack);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "BubblePinwheel")
        {
            ChangeForm(BunnyType.Pinwheel);
            Destroy(col.gameObject);
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
            if (!dying)
            {
                selected = true;
                dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
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
                    if (!col.gameObject.GetComponent<Bunny>().dying)
                    {
                        dragStart = gameObject.transform.position;
                        dragEnd = col.gameObject.transform.position;

                        float sign = Mathf.Sign(dragStart.x - dragEnd.x);
                        RaycastHit2D[] colliders = Physics2D.RaycastAll(dragStart, new Vector2(dragStart.x - dragEnd.x, 0), (dragEnd.x - dragStart.x) * sign);

                        bool anyMoving = false;

                        for (int i = 0; i < colliders.Length; ++i)
                        {
                            if (colliders[i].transform.GetComponent<Bunny>().Moving)
                            {
                                anyMoving = true;
                            }
                        }

                        if (!anyMoving)
                        {
                            StartCoroutine(MoveToPosition(dragEnd));

                            for (int i = 0; i < colliders.Length; ++i)
                            {
                                Debug.Log(colliders.Length);
                                if (colliders[i].transform.name == "Bunny" && colliders[i].transform != gameObject.transform)
                                {
                                    StartCoroutine(colliders[i].transform.gameObject.GetComponent<Bunny>().MoveForward(sign));
                                }
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

        gameObject.transform.position = origPos + new Vector2(1, 0) * sign;
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
