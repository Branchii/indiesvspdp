using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	public string buttonAction;
	bool mouseOver = false;
	public GameObject windowToHideWhenPressed;
	SpriteRenderer spr;
	public Sprite sprite1, sprite2;

	void Awake()
	{
		spr = GameObject.Find("System/Mute Button/Sprite").GetComponent<SpriteRenderer>();
		//spr.sprite = sprite2;
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
				//Debug.Log("Game started");
				Global.StartGame();
				break;
			}
			case "ToggleMusic":
			{
				Global.musCont.ToggleMusic();
				if (spr.sprite == sprite1)
				{
					spr.sprite = sprite2;
				}
				else
				{
					spr.sprite = sprite1;
				}
				//Debug.Log("ToggleMusic");
				break;
			}
			case "ExitGame":
			{
				Application.Quit();
				//Debug.Log("ToggleMusic");
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
