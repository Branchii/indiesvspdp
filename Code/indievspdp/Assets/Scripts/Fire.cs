using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{
	void Update () 
    {
        transform.position += new Vector3(-2.0f * Time.deltaTime, 0, 0);
	}

    public void Extinguish()
    {
        Debug.Log("Fire was put out");
        Destroy(gameObject);
    }
}
