using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	public string buttonAction;
	bool mouseOver = false;
	public GameObject windowToHideWhenPressed;

	void Awake()
	{
		
	}

	void Start ()
	{
	
	}

	void ButtonAction(string action)
	{
		switch (action)
		{
			case "StartGame":
			{
				Debug.Log("Game started");
				Global.StartGame();
				break;
			}
			case "ToggleMusic":
			{
				Global.musCont.ToggleMusic();
				Debug.Log("ToggleMusic");
				break;
			}
			default:
			{
				break;
			}
		}
		if (windowToHideWhenPressed)
		{
			windowToHideWhenPressed.SetActive(false);
		}
	}
	
	void Update ()
	{
		if (mouseOver)
		{
			if (Input.GetMouseButtonDown(0))
			{
				ButtonAction(buttonAction);
			}
		}
	}

	void OnMouseEnter()
	{
		mouseOver = true;
	}

	void OnMouseExit()
	{
		mouseOver = false;
	}
}
