using UnityEngine;
using System.Collections;

public class HazardGenerator : MonoBehaviour
{
	public float rate;				//generation rate
	public float startingTime;		//time before the generator starts (for letting player to gather few bubbles)
	public GameObject[] hazards;	//the hazards, 0 = lumber, 1 = fireman, 2 = pinwheel

	float nextHazard;				//time left before creating next hazard
	Transform hazardPoint;			//where to create hazards

	bool toggleSpawning;

	void Awake()
	{
		Global.hazGen = GetComponent<HazardGenerator>();
	}

	void Start ()
	{
		toggleSpawning = false;
		nextHazard = startingTime;
		hazardPoint = transform;
	}

	public void ToggleSpawning(bool val_)
	{
		if (val_)
		{
			nextHazard = startingTime;
			toggleSpawning = true;
		}
		else
		{
			toggleSpawning = false;
		}
	}

	GameObject GetRandomHazardFromList(ref GameObject[] hazardList_)
	{
		if (hazardList_ != null && hazardList_.Length != 0)
		{
			int i = Random.Range(0, hazardList_.Length);

			if (hazardList_[i] != null)
				return hazardList_[i];
		}
		return null;
	}

	GameObject GetRandomHazardFromListFairly(ref GameObject[] hazardList_)
	{
		if (hazardList_ != null && hazardList_.Length != 0)
		{
			float treeChance = 33.33f;
			float fireChance = 33.33f;
			float rainChance = 33.33f;

			float noItemValue = 10.0f;

			if (Global.bunnyList.ReturnItemCount(Bunny.BunnyType.Lumberjack) < 1) //no lumbers
			{
				treeChance -= noItemValue;
			}
			if (Global.bunnyList.ReturnItemCount(Bunny.BunnyType.Fireman) < 1) //no firemen
			{
				fireChance -= noItemValue;
			}
			if (Global.bunnyList.ReturnItemCount(Bunny.BunnyType.Pinwheel) < 1) //no pinwheels
			{
				rainChance -= noItemValue;
			}

			float value = Random.value * (treeChance + fireChance + rainChance);
			int hazType = -1;

			if (value < treeChance)
			{
				//Debug.Log("tree, " + value + " < " + treeChance);
				hazType = (int)Bunny.BunnyType.Lumberjack - 1;
			}
			else if (value < (treeChance+fireChance))
			{
				hazType = (int)Bunny.BunnyType.Fireman - 1;
			}
			else
			{
				hazType = (int)Bunny.BunnyType.Pinwheel - 1;
			}

			//Debug.Log("rand: " + value);
			//Debug.Log("tc: " + treeChance + " fc: " + fireChance + " rc: " + rainChance);
			//Debug.Log("hazType: " + hazType);

			return hazardList_[hazType];
		}
		return null;
	}



	void CreateRandomHazard()
	{
		GameObject hazardObj = GetRandomHazardFromListFairly(ref hazards); 
		Instantiate(hazardObj, hazardPoint.position, Quaternion.identity);
	}

	void Update ()
	{
		if (toggleSpawning)
			nextHazard -= Time.deltaTime;

		if (nextHazard <= 0.0f)
		{
			CreateRandomHazard();
			nextHazard = rate;
		}
	}
}
