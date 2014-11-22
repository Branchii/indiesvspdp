using UnityEngine;
using System.Collections;

public class DragAndDropHandle : MonoBehaviour
{
	Vector2 tBegin, tEnd;
	Rigidbody2D rigid;

	bool mouseOver = false;
	bool started = false;

	int randomForceTimer = 0;

	void Start ()
	{
		
	}

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		if (mouseOver)
		{
			if (Input.GetMouseButtonDown(0))
			{
				tBegin = Input.mousePosition;
				started = true;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			tEnd = Input.mousePosition;
			if (started)
				GiveForce(tBegin, tEnd);
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
			//Debug.Log("ada");
		}
	}

	void GiveForce(Vector2 begin, Vector2 end)
	{
		if (rigid)
		{
			Vector2 force = -(begin - end).normalized;
			force *= 220.0f;
			rigid.AddForce(force);

			//Debug.Log(force);
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
