using UnityEngine;
using System.Collections;


public class DifficultyControl : MonoBehaviour
{
	public float scrollChangePerTick;
	public int speedUpTicks;
	int speedUpTimer;
	float currentDifficulty; //multiplier

	public float bubbleChangePertick; //change bubble generator rate
	public float hazardChangePerTick; //change hazard generator rate
	void Start ()
	{
		currentDifficulty = 1.0f;
		speedUpTimer = 0;
	}

	void Awake()
	{
		
	}
	
	void FixedUpdate()
	{
		//Global.UICont.score += 1;
		speedUpTimer++;
		if (speedUpTimer >= speedUpTicks)
		{
			Global.sCont.scrollingSpeed += scrollChangePerTick;
			if (Global.bubGen.rate > 0.7)
				Global.bubGen.rate += bubbleChangePertick;
			if (Global.hazGen.rate > 0.9)
			Global.hazGen.rate += hazardChangePerTick;
			speedUpTimer = 0;


			//Debug.Log("scrolling speed: " + Global.sCont.scrollingSpeed);
		}
	}
}
