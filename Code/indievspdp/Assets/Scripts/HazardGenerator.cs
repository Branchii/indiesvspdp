using UnityEngine;
using System.Collections;

public class HazardGenerator : MonoBehaviour
{
	public GameObject[] hazards;	//the hazards

	float nextHazard;				//time left before creating next hazard <---scrollunits
	Transform hazardPoint;			//where to create hazards

	void Start ()
	{
		nextHazard = 2.0f;
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
			nextHazard = 2.0f;
		}
	}
}
