using UnityEngine;
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
	static Vector2 ScreenToWorld(Vector2 screen)
	{
		return new Vector2();
	}
}
