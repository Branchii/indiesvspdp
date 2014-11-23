using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour 
{
    Animator bubbleAnim;

    void Awake()
    {
        bubbleAnim = gameObject.GetComponent<Animator>();
        bubbleAnim.enabled = false;
    }

    public void Pop()
    {
        Destroy(transform.parent.collider2D);
        Destroy(transform.parent.rigidbody2D);
        Destroy(transform.parent.GetComponent<Scroller>());
        Destroy(transform.parent.GetComponent<DragAndDropHandle>());
        bubbleAnim.enabled = true;
    }

    void DestroyBubble()
    {
        Destroy(transform.parent.gameObject);
    }
}
