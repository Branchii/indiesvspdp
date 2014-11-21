using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour
{
	public float objectDeletePointOffset = 0.0f;	//if big objects delete too soon, lower this
	Rigidbody2D rigid;
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	void Awake()
	{
		
	}
	
	void FixedUpdate()
	{
		if (rigid)
		{
			rigid.velocity = new Vector3(-Global.sCont.scrollingSpeed, 0.0f, 0.0f);
		}
		else
		{
			transform.position += new Vector3(-Global.sCont.scrollingSpeed, 0.0f, 0.0f) * Time.deltaTime;
		}

		if (transform.position.x < Global.sCont.scrollingObjectDeletePointX + objectDeletePointOffset)
		{
			Destroy(this.gameObject);
		}
	}
}
