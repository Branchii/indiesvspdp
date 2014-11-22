using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour
{
	public int score = 0;
	public int hazard = 0;

	GameObject scoreObj, timeObj, hazardObj;
	GUIText scoreText, timeText, hazardText;
	float gameTime = 0.0f;
	bool timerToggle = true;

	void Awake ()
	{
		Global.UICont = GetComponent<UIControl>();
	}

	void AddHazardBaseScore()
	{
		score += Global.prefCont.scoreRewardFromHazard;
		hazard++;
	}

	public void BubblePickup()
	{
		score += Global.prefCont.scoreRewardFromBubble;
	}

	public void HazardFireDisable()
	{
		AddHazardBaseScore();
	}

	public void HazardTreeDisable()
	{
		AddHazardBaseScore();
	}

	public void HazardRainDisable()
	{
		AddHazardBaseScore();
	}

	public void ResetTimer()
	{
		gameTime = 0.0f;
	}

	void Update()
	{
		if (timerToggle)
			gameTime += Time.deltaTime;
	}
	
	void FixedUpdate ()
	{
		GameObject.Find("UI/UI Score").GetComponent<GUIText>().text = "" + score;
		GameObject.Find("UI/UI Hazard").GetComponent<GUIText>().text = "" + hazard;

		string timeStr = "";
		int minutes = (int)gameTime / 60;
		int seconds = (int)gameTime % 60;

		if (seconds < 10)
			timeStr = "" + minutes + ":0" + seconds;
		else
			timeStr = "" + minutes + ":" + seconds;

		GameObject.Find("UI/UI Time").GetComponent<GUIText>().text = timeStr;
	}
}
