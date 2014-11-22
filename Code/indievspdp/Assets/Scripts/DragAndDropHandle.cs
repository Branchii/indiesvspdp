using UnityEngine;
using System.Collections;

public class DragAndDropHandle : MonoBehaviour
{
	public float swipeForce;
	Vector2 tBegin, tEnd;
	Vector3 tBeginT, tEndT;
	Rigidbody2D rigid;

	bool mouseOver = false;
	bool started = false;
	bool swiped = true;

	int randomForceTimer = 0;
	Scroller scroller;

	float swipeTimer;


	void Start ()
	{
		swipeTimer = 0.0f;
	}

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		scroller = GetComponent<Scroller>();
	}
	
	void Update ()
	{
		if (started)
		{
			swipeTimer += Time.deltaTime;
		}
		if (mouseOver)
		{
			if (Input.GetMouseButtonDown(0))
			{
				tBegin = Input.mousePosition;
				//tBeginT = transform.position;
				started = true;
			}
		}
		if (Input.GetMouseButtonUp(0) && started)
		{
			tEnd = Input.mousePosition;
			
			if (swipeTimer < 0.4f)
			{
				GiveForce(tBegin, tEnd);
				
			}
			swipeTimer = 0.0f;
			started = false;
		}
	}

	void FixedUpdate()
	{
		randomForceTimer++;
		if (randomForceTimer > 20)
		{
			float val = Random.value * 2.0f - 1.0f;
			float val2 = Random.value * 2.0f - 1.0f;
			rigid.AddForce(new Vector2(val * 8.0f, val2 * 8.0f));
			randomForceTimer = 0;
		}
		if (swiped && rigid.velocity.magnitude < 1.0f)
		{
			if (scroller)
			{
				scroller.enabled = true;
				swiped = false;
				//Debug.Log("scroller enabled");
			}
		}
	}

	void GiveForce(Vector2 begin, Vector2 end)
	{
		if (rigid)
		{
			if (scroller)
			{
				scroller.enabled = false;
			}
			Vector2 force = -(begin - end) * 5.0f;

			/*Vector2 force = -(begin - end).normalized;
			force *= swipeForce; //220.0f;*/
			rigid.AddForce(force);
			swiped = true;
		}
	}

	void OnMouseDown()
	{
		/*foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began)
			{
				tBegin = t.position;
				Debug.Log("begin: " + tBegin);
			}
			else if (t.phase == TouchPhase.Ended)
			{
				tEnd = t.position;
				Debug.Log("end: " + tEnd);
				GiveForce(tBegin, tEnd);
			}
		}*/
	}

	void OnMouseEnter()
	{
		mouseOver = true;
	}
	void OnMouseExit()
	{
		mouseOver = false;
	}
}
