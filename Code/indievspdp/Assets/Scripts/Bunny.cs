using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bunny : MonoBehaviour
{
    const float BUNNY_SWAP_SPEED = 0.2f;
    const int MAX_ITEMS = 2;

    public enum BunnyType
    {
        None,
        Lumberjack,
        Fireman,
        Pinwheel
    }

    TextMesh thing;

    BunnyType bunnyType = BunnyType.None;
    BunnyAnimation bunnyAnim;
    Vector2 dragStart;
    Vector2 origPos;
    public Vector2 newPos;
    Ray2D ray = new Ray2D(Vector2.zero, Vector2.right);
    Queue<int> coRoutineQueue = new Queue<int>();
    int itemAmount = 0;

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
        thing = gameObject.GetComponentInChildren<TextMesh>();
        bunnyAnim = gameObject.GetComponentInChildren<BunnyAnimation>();
        newPos = Vector2.zero;
        origPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (!dying)
        {
            int layerMask = 1 << 8;
            RaycastHit2D obstacle = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1.0f, layerMask);

            if (obstacle)
            {
                if (obstacle.collider.transform.tag == "HazardFire")
                {
                    if (bunnyType == BunnyType.Fireman)
                    {
                        obstacle.collider.gameObject.GetComponentInChildren<HazardAnimation>().Activate();
                        Global.UICont.HazardFireDisable();
                        itemAmount--;
                        thing.text = (itemAmount).ToString();
                    }
                    else
                    {
                        bunnyAnim.DeathAnimation();
                        Dying = true;
                        obstacle.collider.gameObject.GetComponentInChildren<HazardAnimation>().Hit();
						Global.UICont.score -= Global.prefCont.scorePenaltyForDying;
                    }
                }
                else if (obstacle.collider.transform.tag == "HazardRain")
                {
                    if (bunnyType == BunnyType.Pinwheel)
                    {
                        obstacle.collider.gameObject.GetComponentInChildren<HazardAnimation>().Activate();
                        Global.UICont.HazardRainDisable();
                        itemAmount--;
                        thing.text = (itemAmount).ToString();
                    }
                    else
                    {
                        bunnyAnim.DeathAnimation();
                        Dying = true;
                        obstacle.collider.gameObject.GetComponentInChildren<HazardAnimation>().Hit();
						Global.UICont.score -= Global.prefCont.scorePenaltyForDying;
                    }
                }
                else if (obstacle.collider.transform.tag == "HazardTree")
                {
                    if (bunnyType == BunnyType.Lumberjack)
                    {
                        obstacle.collider.gameObject.GetComponentInChildren<HazardAnimation>().Activate();
                        Global.UICont.HazardTreeDisable();
                        itemAmount--;
                        thing.text = (itemAmount).ToString();
                    }
                    else
                    {
                        bunnyAnim.DeathAnimation();
                        Dying = true;
                        obstacle.collider.gameObject.GetComponentInChildren<HazardAnimation>().Hit();
						Global.UICont.score -= Global.prefCont.scorePenaltyForDying;
                    }
                }

                if (itemAmount == 0)
                {
                    ChangeForm(BunnyType.None);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BubbleWater")
        {
            ChangeForm(BunnyType.Fireman);
            col.gameObject.GetComponentInChildren<Bubble>().Pop();
            Global.UICont.BubblePickup();
        }
        else if (col.gameObject.tag == "BubbleSaw")
        {
            ChangeForm(BunnyType.Lumberjack);
            col.gameObject.GetComponentInChildren<Bubble>().Pop();
            Global.UICont.BubblePickup();
        }
        else if (col.gameObject.tag == "BubblePinwheel")
        {
            ChangeForm(BunnyType.Pinwheel);
            col.gameObject.GetComponentInChildren<Bubble>().Pop();
            Global.UICont.BubblePickup();
        }
        else if (col.gameObject.tag == "BubbleUp")
        {
            gameObject.GetComponentInParent<BunnyList>().AddBunny();
            col.gameObject.GetComponentInChildren<Bubble>().Pop();
            Global.UICont.BubblePickup();
        }
    }

    void ChangeForm(BunnyType type)
    {
        if (bunnyType == type)
        {
            if (bunnyType != BunnyType.None)
            {
                if (itemAmount < MAX_ITEMS)
                {
                    itemAmount++;
                    thing.text = itemAmount.ToString();
                }
            }
        }
        else
        {
            bunnyType = type;
            bunnyAnim.ChangeAnimation(bunnyType);
            if (bunnyType == BunnyType.None)
            {
                itemAmount = 0;
                thing.text = "";
            }
            else
            {
                itemAmount = 1;
                thing.text = itemAmount.ToString();
            }
        }
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseDown()
    {
        if (mouseOver)
        {
            if (!dying && !Moving)
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
                float sign = Mathf.Sign(dragStart.x - dragEnd.x);
                RaycastHit2D[] colliders = Physics2D.RaycastAll(dragStart, new Vector2(dragStart.x - dragEnd.x, 0), (dragEnd.x - dragStart.x) * sign);
                Collider2D lastBunny = gameObject.collider2D;
                List<Collider2D> bunnies = new List<Collider2D>();
    
                foreach (RaycastHit2D rch in colliders)
                {
                    if (rch.transform.name == "Bunny")
                    {
                        bunnies.Add(rch.collider);
    
                        if (sign == -1)
                        {
                            if (rch.collider.transform.position.x > lastBunny.transform.position.x)
                            {
                                lastBunny = rch.collider;
                            }
                        }
                        else if (sign == 1)
                        {
                            if (rch.collider.transform.position.x < lastBunny.transform.position.x)
                            {
                                lastBunny = rch.collider;
                            }
                        }
                    }
                }
    
                if (lastBunny != gameObject.collider2D)
                {
                    if (!lastBunny.gameObject.GetComponent<Bunny>().dying)
                    {
                        dragStart = gameObject.transform.position;
                        dragEnd = lastBunny.transform.position;
    
                        bool anyMoving = false;
    
                        for (int i = 0; i < bunnies.Count; ++i)
                        {
                            if (bunnies[i].transform.GetComponent<Bunny>().Moving)
                            {
                                anyMoving = true;
                            }
                        }
    
                        if (!anyMoving)
                        {
                            StartCoroutine(MoveToPosition(dragEnd));
    
                            for (int i = 0; i < bunnies.Count; ++i)
                            {
                                if (bunnies[i].transform.name == "Bunny" && bunnies[i].transform != gameObject.transform)
                                {
                                    StartCoroutine(bunnies[i].transform.gameObject.GetComponent<Bunny>().MoveForward(sign));
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
        int queueNumber = coRoutineQueue.Count;

        coRoutineQueue.Enqueue(queueNumber);
        while (coRoutineQueue.Peek() != queueNumber)
        {
            yield return null;
        }
        Moving = true;

        newPos = origPos + new Vector2(BunnyList.BUNNY_INTERVAL, 0) * sign;

        for (float f = 0f; f < BUNNY_SWAP_SPEED; f += Time.deltaTime)
        {
            gameObject.transform.position = origPos + new Vector2(BunnyList.BUNNY_INTERVAL, 0) * (f / BUNNY_SWAP_SPEED) * sign;
            yield return null;
        }
        gameObject.transform.position = origPos + new Vector2(BunnyList.BUNNY_INTERVAL, 0) * sign;
        origPos = gameObject.transform.position;
        Moving = false;

        coRoutineQueue.Dequeue();
        newPos = Vector2.zero;
    }

    public IEnumerator MoveToPosition(Vector2 pos)
    {
        int queueNumber = 200;
        coRoutineQueue.Enqueue(queueNumber);

        while (coRoutineQueue.Peek() != queueNumber)
        {
            yield return null;
        }

        Moving = true;

        newPos = pos;

        for (float f = 0f; f < BUNNY_SWAP_SPEED; f += Time.deltaTime)
        {
            gameObject.transform.position = origPos + (pos - origPos) * (f / BUNNY_SWAP_SPEED);
            yield return null;
        }

        gameObject.transform.position = pos;
        origPos = gameObject.transform.position;

        Moving = false;

        coRoutineQueue.Dequeue();
        newPos = Vector2.zero;
    }

    public BunnyType ReturnType()
    {
        return bunnyType;
    }
}
