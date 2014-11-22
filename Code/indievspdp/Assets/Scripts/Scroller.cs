using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour
{
	public float objectDeletePointOffset;	//if big objects delete too soon, lower this

	public float rigidDrag;					//add drag to scrolling, allowing to customize rigid's own drag
	Vector2 lastVelo = new Vector3();
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
			//rigid.velocity -= lastVelo;
			//rigid.velocity += new Vector2(-Global.sCont.scrollingSpeed, 0.0f);
			//lastVelo = new Vector2(-Global.sCont.scrollingSpeed, 0.0f); ;

			//rigid.velocity += new Vector2(-Global.sCont.scrollingSpeed*0.1f, 0.0f);

			rigid.AddForce(new Vector2(-Global.sCont.scrollingSpeed * 1.0f, 0.0f));
			rigid.AddForce(new Vector2(rigidDrag, 0.0f));

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
