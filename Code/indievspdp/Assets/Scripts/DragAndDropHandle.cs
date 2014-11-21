using UnityEngine;
using System.Collections;

public class DragAndDropHandle : MonoBehaviour
{
	Vector2 tBegin, tEnd;
	Rigidbody2D rigid;

	bool mouseOver = false;

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
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			tEnd = Input.mousePosition;
			GiveForce(tBegin, tEnd);
		}
	}

	void GiveForce(Vector2 begin, Vector2 end)
	{
		if (rigid)
		{
			rigid.velocity = begin - end;
			Debug.Log((begin - end));
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
