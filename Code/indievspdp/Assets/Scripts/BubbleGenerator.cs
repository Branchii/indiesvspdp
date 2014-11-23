using UnityEngine;
using System.Collections;

public class BubbleGenerator : MonoBehaviour
{
	public float rate;					//generation rate
	public GameObject[] bubbles;		//the bubbles
	public GameObject bunnyUp;			//bunny up object
	public float randomRadius;			//randomize spawn points

	float nextbubble;					//time left before creating next bubbles
	Transform bubblePoint;				//where to create bubbles

	void Awake()
	{
		Global.bubGen = GetComponent<BubbleGenerator>();
	}

	void Start()
	{
		nextbubble = 0.0f;
		bubblePoint = transform;
		CreateAllBubbles();
	}

	void CreateAllBubbles()
	{
		for (int i = 0; i < bubbles.Length; i++)
		{
			CreateBubble(bubbles[i]);
		}
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

	GameObject CreateBubble(GameObject obj_)
	{
		Vector3 randomPos = new Vector3((Random.value*2.0f-1.0f) * randomRadius,(Random.value*2.0f-1.0f) * randomRadius, 0.0f);



		//GameObject hazardObj = GetRandomBubbleFromList(ref bubbles);
		GameObject thisBubble = Instantiate(obj_, bubblePoint.position + randomPos, Quaternion.identity) as GameObject;
		thisBubble.GetComponent<DragAndDropHandle>().swipeForce = Global.prefCont.bubbleSwipeForce;
		thisBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50.0f, 0.0f));
		thisBubble.GetComponent<Scroller>().scrollingSpeedOffset = 0.15f;
		return thisBubble;
	}

	void CreateRandomBubble()
	{
		GameObject hazardObj = GetRandomBubbleFromList(ref bubbles);
		CreateBubble(hazardObj);


		/*GameObject thisBubble = Instantiate(hazardObj, bubblePoint.position, Quaternion.identity) as GameObject;
		thisBubble.GetComponent<DragAndDropHandle>().swipeForce = Global.prefCont.bubbleSwipeForce;
		thisBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50.0f, 0.0f));
		thisBubble.GetComponent<Scroller>().scrollingSpeedOffset = 0.15f;*/

		//bunnyup?
		float value = Random.value * 100.0f;
		float thres = 0.0f;
		int bunnies = Global.bunnyList.childCount;

		//uncomment 1,2 and 3 to print probabilities while tweaking
		//for (int bn = 1; bn < 9; bn++ ) //1
		{
			//bunnies = bn; //2
			if (bunnies < 3)
			{
				thres = 15.0f; //% spawnrate
			}
			else if (bunnies >= 6) //more than six
			{
				thres = 3.0f;
			}
			else
			{
				thres = 15.0f - bunnies * 1.5f;
			}
			//Debug.Log("probabdebug: bunnies: " + bn + "thres:" + thres); //3
		}
		if (value < thres)
		{
			CreateBubble(bunnyUp);
			/*GameObject thisBubble2 = Instantiate(bunnyUp, bubblePoint.position, Quaternion.identity) as GameObject;
			thisBubble.GetComponent<DragAndDropHandle>().swipeForce = Global.prefCont.bubbleSwipeForce;
			thisBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50.0f, 0.0f));
			thisBubble.GetComponent<Scroller>().scrollingSpeedOffset = 0.15f;*/
		}
		
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
