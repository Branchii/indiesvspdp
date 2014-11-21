﻿using UnityEngine;
using System.Collections;

public class BubbleGenerator : MonoBehaviour
{
	public GameObject[] bubbles;	//the bubbles

	float nextbubble;				//time left before creating next bubbles
	Transform bubblePoint;			//where to create bubbles

	void Start()
	{
		nextbubble = 2.0f;
		bubblePoint = transform;
	}
	GameObject GetRandomBubbleFromList(ref GameObject[] bubbleList_)
	{
		if (bubbleList_ != null && bubbleList_.Length != 0)
		{
			int i = Random.Range(0, bubbleList_.Length);

			if (bubbleList_[i] != null)
				return bubbleList_[i];
		}
		return null;
	}

	void CreateRandomBubble()
	{
		GameObject hazardObj = GetRandomBubbleFromList(ref bubbles);
		Instantiate(hazardObj, bubblePoint.position, Quaternion.identity);
	}

	void Update()
	{
		nextbubble -= Time.deltaTime;
		if (nextbubble <= 0.0f)
		{
			CreateRandomBubble();
			nextbubble = 2.0f;
		}
	}
}
