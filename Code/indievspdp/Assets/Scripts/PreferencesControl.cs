using UnityEngine;
using System.Collections;

public class PreferencesControl : MonoBehaviour
{
	public float bubbleSwipeForce;

	public int scoreRewardFromHazard;
	public float scoreRewardPerSecond;
	public int scoreRewardFromBubble;
    public int scoreRewardFromExtraBunnyUp;
	public int scorePenaltyForDying;

	void Awake()
	{
		Global.prefCont = GetComponent<PreferencesControl>();
	}
}
