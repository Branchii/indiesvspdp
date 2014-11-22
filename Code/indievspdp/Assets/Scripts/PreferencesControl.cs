using UnityEngine;
using System.Collections;

public class PreferencesControl : MonoBehaviour
{
	public float bubbleSwipeForce;

	void Start()
	{
		Global.prefCont = GetComponent<PreferencesControl>();
	}
}
