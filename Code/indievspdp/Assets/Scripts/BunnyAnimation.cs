﻿using UnityEngine;
using System.Collections;

public class BunnyAnimation : MonoBehaviour 
{
    Bunny bunnyScript;
    Animator anim;
    Bunny.BunnyType bunnyType;

    void Awake()
    {
        anim = GetComponent<Animator>();
        bunnyScript = gameObject.GetComponentInParent<Bunny>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeAnimation(Bunny.BunnyType type)
    {
        switch (bunnyType)
        {
            case Bunny.BunnyType.None:
                anim.SetBool("default", false);
                break;

            case Bunny.BunnyType.Fireman:
                anim.SetBool("fireman", false);
                break;

            case Bunny.BunnyType.Constructor:
                anim.SetBool("constructor", false);
                break;

            case Bunny.BunnyType.Lumberjack:
                anim.SetBool("lumberjack", false);
                break;
        }

        bunnyType = type;

        switch (bunnyType)
        {
            case Bunny.BunnyType.None:
                anim.SetBool("default", true);
                break;

            case Bunny.BunnyType.Fireman:
                anim.SetBool("fireman", true);
                break;

            case Bunny.BunnyType.Constructor:
                anim.SetBool("constructor", true);
                break;

            case Bunny.BunnyType.Lumberjack:
                anim.SetBool("lumberjack", true);
                break;
        }
    }

    public void DeathAnimation()
    {
        anim.SetBool("death", true);
    }

    void Death()
    {
        Destroy(transform.parent.gameObject);
    }
}