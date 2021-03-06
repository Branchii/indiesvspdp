﻿using UnityEngine;
using System.Collections;

static public class Global
{
	static public ScrollerControl sCont = null;
	static public BubbleGenerator bubGen = null;
	static public HazardGenerator hazGen = null;
	static public PreferencesControl prefCont = null;
	static public Camera cam = null;
	static public UIControl UICont = null;
	static public BunnyList bunnyList = null;
	static public MusicControl musCont = null;
	static public GameObject uiObj = null;

	static Vector2 ScreenToWorld(Vector2 screen)
	{
		return new Vector2();
	}


	static public void StartGame()
	{
		UICont.ResetTimer();
		UICont.timerToggle = true;
		bubGen.ToggleSpawning(true);
		hazGen.ToggleSpawning(true);
		
		//reset score
		UICont.score = 0;
		UICont.hazard = 0;
        
        bunnyList.StartingBunnies(3); //parameter is the amount of bunnies spawned at start

		//show ui (hidden from beginning of the game, and never being hidden again)
		UICont.ShowUI();

		hazGen.DeleteAll();
		bubGen.DeleteAll();
		sCont.scrollingSpeed = 1.52f;
		bubGen.rate = 2.1f;
		hazGen.rate = 4.5f;

	}
	static public void StopGame()
	{
		UICont.timerToggle = false;
		bubGen.ToggleSpawning(false);
		hazGen.ToggleSpawning(false);

		//show gameover screen
		UICont.ShowGameOverWindow();
		
	}
}
