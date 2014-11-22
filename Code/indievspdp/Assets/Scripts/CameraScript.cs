using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	void Start ()
	{
		Global.cam = GetComponent<Camera>();
	}
}
