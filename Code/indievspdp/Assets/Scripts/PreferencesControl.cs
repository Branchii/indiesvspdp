using UnityEngine;
using System.Collections;

public class PreferencesControl : MonoBehaviour
{
	public float bubbleSwipeForce;

	public int scoreRewardFromHazard;
	public float scoreRewardPerSecond;
	public int scoreRewardFromBubble;

	void Start()
	{
		Global.prefCont = GetComponent<PreferencesControl>();
	}
}
