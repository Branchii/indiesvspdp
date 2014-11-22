using UnityEngine;
using System.Collections;

public class BubbleGenerator : MonoBehaviour
{
	public float rate;				//generation rate
	public GameObject[] bubbles;	//the bubbles

	float nextbubble;				//time left before creating next bubbles
	Transform bubblePoint;			//where to create bubbles

	void Start()
	{
		Global.bubGen = GetComponent<BubbleGenerator>();
		nextbubble = 0.0f;
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
		GameObject thisBubble = Instantiate(hazardObj, bubblePoint.position, Quaternion.identity) as GameObject;
		thisBubble.GetComponent<DragAndDropHandle>().swipeForce = Global.prefCont.bubbleSwipeForce;
		thisBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50.0f, 0.0f));
	}

	void Update()
	{
		nextbubble -= Time.deltaTime;
		if (nextbubble <= 0.0f)
		{
			CreateRandomBubble();
			nextbubble = rate;
		}
	}
}
