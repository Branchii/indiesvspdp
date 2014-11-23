using UnityEngine;
using System.Collections;

public class BackgroundTeleporter : MonoBehaviour
{
    float elementWidth;
    public GameObject otherElement;

    void Awake()
    {
        elementWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.x < (Global.sCont.scrollingObjectDeletePointX - (elementWidth / 2)))
        {
            gameObject.transform.position = new Vector2(otherElement.transform.position.x + elementWidth, otherElement.transform.position.y);
        }
    }
}