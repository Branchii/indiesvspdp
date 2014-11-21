using UnityEngine;
using System.Collections;

public class ScrollerControl : MonoBehaviour
{
	public float scrollingSpeed, scrollingObjectDeletePointX;
	void Start ()
	{
		Global.sCont = GetComponent<ScrollerControl>();
	}

	float GetScrollingSpeed()
	{
		return scrollingSpeed;
	}

	void Update ()
	{
	
	}
}
