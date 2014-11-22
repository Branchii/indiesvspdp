using UnityEngine;
using System.Collections;

public class HazardGenerator : MonoBehaviour
{
	public float rate;				//generation rate
	public float startingTime;		//time before the generator starts (for letting player to gather few bubbles)
	public GameObject[] hazards;	//the hazards

	float nextHazard;				//time left before creating next hazard
	Transform hazardPoint;			//where to create hazards

	void Start ()
	{
		Global.hazGen = GetComponent<HazardGenerator>();
		nextHazard = startingTime;
		hazardPoint = transform;
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

	void CreateRandomHazard()
	{
		GameObject hazardObj = GetRandomHazardFromList(ref hazards); 
		Instantiate(hazardObj, hazardPoint.position, Quaternion.identity);
	}

	void Update ()
	{
		nextHazard -= Time.deltaTime;
		if (nextHazard <= 0.0f)
		{
			CreateRandomHazard();
			nextHazard = rate;
		}
	}
}
