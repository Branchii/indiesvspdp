using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	void Awake ()
	{
		Global.cam = GetComponent<Camera>();
	}
}
