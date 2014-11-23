using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour
{
	public float objectDeletePointOffset;	//if big objects delete too soon, lower this
	public float rigidDrag;					//add drag to scrolling, allowing to customize rigid's own drag
	public float scrollingSpeedOffset;			//add to scrollingspeed independently
    public bool indestructible;

	Rigidbody2D rigid;

	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (rigid)
		{
			rigid.AddForce(new Vector2(-Global.sCont.scrollingSpeed - scrollingSpeedOffset, 0.0f));
			rigid.AddForce(new Vector2(rigidDrag, 0.0f));

		}
		else
		{
			transform.position += new Vector3(-Global.sCont.scrollingSpeed, 0.0f, 0.0f) * Time.deltaTime;
		}

        if (!indestructible)
        {
            if (transform.position.x < Global.sCont.scrollingObjectDeletePointX + objectDeletePointOffset)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
